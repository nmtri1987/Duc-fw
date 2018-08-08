using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Runtime.Caching;

namespace Helpers
{
    public class HttpCache
    {
        #region Fields
        private static readonly Cache _cache;
        #endregion

        #region Ctor

        protected static ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        /// <summary>
        /// Creates a new instance of the NopCache class
        /// </summary>
        static HttpCache()
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                _cache = current.Cache;
            }
            else
            {
                _cache = HttpRuntime.Cache;
            }
        }

        /// <summary>
        /// Creates a new instance of the NopCache class
        /// </summary>
        private HttpCache()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Removes all keys and values from the cache
        /// </summary>
        public static void Clear()
        {
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                _cache.Remove(enumerator.Key.ToString());
            }
        }
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public static object Get(string key)
        {
            return _cache[key];
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">object</param>
        public static void Max(string key, object obj)
        {
            Max(key, obj, null);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">object</param>
        /// <param name="dep">cache dependency</param>
        public static void Max(string key, object obj, CacheDependency dep)
        {
            if (IsEnabled && (obj != null))
            {
                _cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            _cache.Remove(key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public static void RemoveByPattern(string pattern)
        {
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Key.ToString()))
                {
                    _cache.Remove(enumerator.Key.ToString());
                }
            }
        }
        public static void RemoveSearchCache(bool AllowSearchCache,string SETTINGS_Search_KEY)
        {
            if (AllowSearchCache)
            {
                foreach (var c in _cache)
                {
                    string key = ((DictionaryEntry)c).Key.ToString();
                    string keyRemove = string.Format(SETTINGS_Search_KEY, "" , "", "", "", "","","");
                    if (key.Contains(keyRemove))
                    {
                        RemoveByPattern(key);
                    }
                }
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public static void NClear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the cache is enabled
        /// </summary>
        public static bool IsEnabled
        {
            get
            {

                return true;
            }
        }
        #endregion
    }
}
