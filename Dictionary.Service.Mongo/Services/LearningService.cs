
using Infrastructure.MongoDb;
using MongoDb.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyDictionary.Services.Services
{
    public class LearningService : GenericService<Example>, ILearningService
    {
        private List<KeyValuePair<string, string>> _paragraphs;
        public List<KeyValuePair<string, string>> Paragraphs { get { return _paragraphs; } }
        private Stack<KeyValuePair<string, string>> _randomParagraphs = new Stack<KeyValuePair<string, string>>(); 
        public LearningService(IUnitOfWork uow) : base(uow)
        {

        }
        public void RefreshParagraphs()
        {
            var newestExample = _mainRepos.Filter(x=>true).Result.OrderByDescending(x => x.CreatedAt).First();
            var tokens = Regex.Split(newestExample.Description, "\r\n").Where(x => x.Trim() != string.Empty).ToArray();
            _paragraphs = new List<KeyValuePair<string, string>>();
            for (var i = 0; i < tokens.Length - 1; i += 2)
            {
                var tokenKey = tokens[i];
                var tokenVal = tokens[i + 1];
                _paragraphs.Add(new KeyValuePair<string, string>(tokenKey, tokenVal));
            }
        }

        public Stack<KeyValuePair<string, string>> InitRandomParagraph()
        {
            RefreshParagraphs();
            var randomIndexes = new List<int>(_paragraphs.Count);
            var i = 0;
            foreach (var p in _paragraphs)
            {
                randomIndexes.Add(i++);
            }
            var n = _paragraphs.Count;
            var rnd = new Random();
            while (n > 1)
            {
                n--;
                var rndIndex = rnd.Next(n + 1);
                var tmp = randomIndexes[rndIndex];
                randomIndexes[rndIndex] = randomIndexes[n];
                randomIndexes[n] = tmp;
            }
            var keyPairValues = new Stack<KeyValuePair<string, string>>();
            foreach(var index in randomIndexes)
            {
                keyPairValues.Push(Paragraphs[index]);
            }

            return keyPairValues;
        } 
    }
}
