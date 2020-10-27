using MyDictionary.DataAccess.Models;
using MyDictionary.Services.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Web.Cached
{
    public class LearningCached : CachedService<Example>
    {
        private Stack<KeyValuePair<string, string>> _randomPages;
        public void InitCached(Stack<KeyValuePair<string, string>> keyValuePairs)
        {
            _randomPages = keyValuePairs;
        }

        public KeyValuePair<string, string> GetNext()
        {
            if (_randomPages == null)
            {
                return default;
            } 

            if (_randomPages.Count == 0)
            {
                return default;
            }

            return _randomPages.Pop();
        }
    }
}
