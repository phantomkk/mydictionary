using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Services.Utils
{
    public class WordUtils
    {
        public static IEnumerable<string> ExtractDistinct(string source)
        {
            var words = source.ToLower().Split(' ','\n').Select(x=>Normalize(x));
            var wordsDistinct = words.Distinct();
            return wordsDistinct.Where(x=>x != string.Empty);
        }
        private static string Normalize(string source)
        {
            var specialChars = new char[] { '.','\n','\r', ',', '\'', '\"', ':', '?', '~' };
            source = source.TrimStart(specialChars);
            source = source.TrimEnd(specialChars);
            source = source.Trim();
            return source.ToLower();
        }
    }
}
