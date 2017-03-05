using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

// Class taken from Sitecore.XA.Feature.Social.dll
// This should be removed if/when merged to the whole XA module

namespace Hackathon.XA.Feature.Social.Providers
{
    public abstract class AbstractFeedProvider<T>
    {
        private static readonly object Lock = new object();

        protected abstract string CacheKey { get; }

        public List<T> GetFeedItems(int cacheInterval, int count)
        {
            string key1 = string.Format("{0}_{1}_{2}", (object)this.CacheKey, (object)count, (object)cacheInterval);
            string key2 = string.Format("{0}_{1}_{2}_backup", (object)this.CacheKey, (object)count, (object)cacheInterval);
            List<T> source = HttpContext.Current.Cache[key1] as List<T>;
            if (source == null)
            {
                lock (AbstractFeedProvider<T>.Lock)
                {
                    source = HttpContext.Current.Cache[key1] as List<T>;
                    if (source == null)
                    {
                        source = this.GetFeedItemsInternal(count);
                        if (source.Any<T>())
                        {
                            HttpContext.Current.Cache.Add(key1, (object)source, (CacheDependency)null, DateTime.UtcNow.AddMinutes((double)cacheInterval), Cache.NoSlidingExpiration, CacheItemPriority.Normal, (CacheItemRemovedCallback)null);
                            HttpContext.Current.Cache.Add(key2, (object)source, (CacheDependency)null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, (CacheItemRemovedCallback)null);
                        }
                        else if (HttpContext.Current.Cache[key2] != null)
                        {
                            source = HttpContext.Current.Cache[key2] as List<T>;
                            Log.Warn("There was an problem with loading Instagram feed items so getting them from backup cache", (object)this);
                        }
                    }
                }
            }
            return source.Take<T>(count).ToList<T>();
        }

        protected abstract List<T> GetFeedItemsInternal(int count);
    }
}