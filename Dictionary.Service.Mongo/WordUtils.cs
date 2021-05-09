using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Web.Utils
{
    public class WordUtils
    {
        public static IEnumerable<string> ExtractDistinct(string source)
        {
            var words = source.Split(' ','\n');
            var wordsDistinct = words.Distinct();
            return wordsDistinct;
        }
    }
}
