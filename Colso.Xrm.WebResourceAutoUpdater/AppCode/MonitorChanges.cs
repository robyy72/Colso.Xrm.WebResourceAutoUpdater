using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;
using System.Reflection;
using System.Threading;
using Microsoft.Xrm.Sdk;
using System.Linq;

namespace Colso.Xrm.WebResourceAutoUpdater.AppCode
{
    public class MonitorChanges
    {
        private string _folder;
        private IOrganizationService _service;

        private FileSystemWatcher _fsw;
        private readonly MemoryCache _memCache = FastExpiringCache.Default;
        private CacheItemPolicy _cacheItemPolicy;
        private const int CacheTimeMilliseconds = 1000;
        static ManualResetEvent _quitEvent = new ManualResetEvent(false);

        private int _activeUploads = 0;
        private List<Guid> _resourceIds = new List<Guid>();
        public event EventHandler OnMonitorMessage;

        public void Start(IOrganizationService service, string folder, string filter)
        {
            _folder = folder;
            _service = service;

            _fsw = new FileSystemWatcher(_folder)
            {
                Filter = filter,
                NotifyFilter = NotifyFilters.LastWrite,
                IncludeSubdirectories = true
            };

            _fsw.Error += new ErrorEventHandler(OnError);
            _fsw.Changed += OnChanged;
            _fsw.EnableRaisingEvents = true;

            _cacheItemPolicy = new CacheItemPolicy() { RemovedCallback = OnRemovedFromCache };

            SetStatusMessage("Watching for file changes in folder '{0}'.", _folder);
            _quitEvent.WaitOne();
        }

        public void Stop()
        {
            SetStatusMessage("Stopping watching folder {0}.", _folder);
            _fsw.EnableRaisingEvents = false;
            _fsw.Dispose();
        }

        public bool IsRunning()
        {
            return _fsw != null && _fsw.EnableRaisingEvents;
        }

        private void OnError(object source, ErrorEventArgs e)
        {
            SetStatusMessage("Error: {0}", e.GetException().Message);
        }

        private void OnRemovedFromCache(CacheEntryRemovedArguments args)
        {
            if (args.RemovedReason != CacheEntryRemovedReason.Expired) return;

            _activeUploads++;

            // Get all files in the cache
            var filenames = new List<string>();
            var e = (FileSystemEventArgs)args.CacheItem.Value;
            filenames.Add(e.Name);

            // Do we already have other files to change?
            foreach (var f in _memCache)
            {
                _memCache.Remove(f.Key);
                var fse = (FileSystemEventArgs)f.Value;
                filenames.Add(fse.Name);
            }

            SetStatusMessage("Start upload");
            var ids = _service.Upload(_folder, filenames.ToArray());
            _resourceIds.AddRange(ids);

            // Only publish when no more files are being uploaded
            _activeUploads--;
            if (_activeUploads == 0)
            {
                var uids = _resourceIds.Distinct().ToArray();
                _resourceIds.Clear();
                SetStatusMessage("Start publish");
                _service.Publish(uids);

                // Add to solution?!
            }

            SetStatusMessage("Done!");
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            _cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddMilliseconds(CacheTimeMilliseconds);
            // Only add if it is not there already (swallow others)
            SetStatusMessage("Changed file: {0}", e.Name.Replace(_folder, string.Empty));
            _memCache.AddOrGetExisting(e.Name, e, _cacheItemPolicy);
        }

        private void SetStatusMessage(string format, params object[] args)
        {
            // Make sure someone is listening to event
            if (OnMonitorMessage == null) return;

            OnMonitorMessage(this, new StatusMessageEventArgs(string.Format(format, args)));
        }
    }
}