using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Web.Cached
{
    public abstract class CachedService<T>
    {
        private int CachedExpireInMinutes = 5;
        private DateTime _expiredCached;
        private IEnumerable<T> _cachedData = null;

        public virtual IEnumerable<T> GetCachedData()
        { 
            if(_expiredCached == null || _expiredCached < DateTime.Now)
            {
                _cachedData = null; 
            }

            return _cachedData;
        }

        public void Clear()
        {
            _cachedData = null;
        }

        public virtual void SaveToCached(IEnumerable<T> data)
        {
            _cachedData = data;
            _expiredCached = DateTime.Now.AddMinutes(CachedExpireInMinutes);
        }
    }
}
