using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Colso.Xrm.WebResourceAutoUpdater.AppCode
{
    public class FastExpiringCache
    {
        public static MemoryCache Default { get; } = Create();

        private static MemoryCache Create()
        {
            MemoryCache instance = null;
            Assembly assembly = typeof(CacheItemPolicy).Assembly;
            Type type = assembly.GetType("System.Runtime.Caching.CacheExpires");
            if (type != null)
            {
                FieldInfo field = type.GetField("_tsPerBucket", BindingFlags.Static | BindingFlags.NonPublic);
                if (field != null && field.FieldType == typeof(TimeSpan))
                {
                    TimeSpan originalValue = (TimeSpan)field.GetValue(null);
                    field.SetValue(null, TimeSpan.FromMilliseconds(1000));
                    instance = new MemoryCache("FastExpiringCache");
                    field.SetValue(null, originalValue); // reset to original value
                }
            }
            return instance ?? new MemoryCache("FastExpiringCache");
        }
    }
}
