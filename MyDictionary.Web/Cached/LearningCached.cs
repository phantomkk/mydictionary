using MongoDb.DataAccess.Entities;
using System.Collections.Generic;

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
