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
        private HashSet<string> _allowedextensions;
        private string _solutionuniquename;
        private IOrganizationService _service;

        private FileSystemWatcher _fsw;
        private readonly MemoryCache _memCache = FastExpiringCache.Default;
        private CacheItemPolicy _cacheItemPolicy;
        private int _cacheTimeMilliseconds = 1000;
        static ManualResetEvent _quitEvent = new ManualResetEvent(false);

        private List<Guid> _resourceIds = new List<Guid>();
        public event EventHandler OnMonitorMessage;

        public void Start(IOrganizationService service, int cachetime, string folder, string solutionuniquename, string[] allowedextensions)
        {
            _cacheTimeMilliseconds = cachetime;
            _solutionuniquename = solutionuniquename;
            _folder = folder;
            _service = service;

            if (allowedextensions != null && allowedextensions.Length > 0)
            {
                _allowedextensions = new HashSet<string>();
                foreach (var ext in allowedextensions)
                    _allowedextensions.Add(ext.TrimStart('.', '*').ToLower());
            }

            _fsw = new FileSystemWatcher(_folder)
            {
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
            if (_fsw != null)
            {
                SetStatusMessage("Stopping watching folder {0}.", _folder);
                _fsw.EnableRaisingEvents = false;
                _fsw.Dispose();
            }
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

            var updates = ((HashSet<string>)args.CacheItem.Value).ToArray();
            var directories = updates
                .Where(u => ((File.GetAttributes(u) & FileAttributes.Directory) == FileAttributes.Directory))
                .ToArray();

            // Get all files in the cache
            var filenames = updates.Where(p => !directories.Any(p2 => p2 == p)).ToList();
            filenames.AddRange(directories.SelectMany(d => Directory.GetFiles(d)));
            filenames = filenames.Where(f => IsExtensionAllowed(f)).Distinct().ToList();

            if (filenames.Count > 0)
            {

                try
                {
                    SetStatusMessage("Start upload of {0} file{1}", filenames.Count, filenames.Count == 1 ? string.Empty : "s");
                    foreach (var f in filenames)
                    {
                        try
                        {
                            _resourceIds.Add(_service.Upload(_folder, f));
                        }
                        catch (Exception ex)
                        {
                            SetStatusMessage("FAILED! {0}", ex);
                        }
                    }

                    var uids = _resourceIds.Distinct().ToArray();
                    _resourceIds.Clear();
                    SetStatusMessage("Start publish");
                    _service.Publish(uids);

                    // Add to solution?!
                    if (!string.IsNullOrEmpty(_solutionuniquename))
                    {
                        _service.AddToSolution(_solutionuniquename, uids);
                    }

                    SetStatusMessage("Done!");
                }
                catch (Exception ex)
                {
                    SetStatusMessage("ERROR! {0}", ex);
                }
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            var attr = File.GetAttributes(e.FullPath);
            var isdirectory = ((attr & FileAttributes.Directory) == FileAttributes.Directory);

            if (isdirectory || IsExtensionAllowed(e.FullPath))
            {
                HashSet<string> currentupdates = (HashSet<string>)_memCache.Get("fileupdates");
                if (currentupdates == null) currentupdates = new HashSet<string>();

                // Only add if it is not there already (swallow others)
                if (!currentupdates.Contains(e.FullPath))
                {
                    SetStatusMessage("Changed {1}: {0}", e.Name, isdirectory ? "directory": "file");
                    currentupdates.Add(e.FullPath);
                }

                _cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddMilliseconds(_cacheTimeMilliseconds);
                _memCache.AddOrGetExisting("fileupdates", currentupdates, _cacheItemPolicy);
            }
        }

        private bool IsExtensionAllowed(string file)
        {
            return (_allowedextensions == null || _allowedextensions.Count == 0 ||
                _allowedextensions.Contains(Path.GetExtension(file).TrimStart('.').ToLower()));
        }

        private void SetStatusMessage(string format, params object[] args)
        {
            // Make sure someone is listening to event
            if (OnMonitorMessage == null) return;

            OnMonitorMessage(this, new StatusMessageEventArgs(string.Format(format, args)));
        }
    }
}