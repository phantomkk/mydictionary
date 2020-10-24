using Crawler.Web.BusinessLayer;
using MyDictionary.DataAccess.Models;
using MyDictionary.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyDictionary.Services.Services
{
    public interface IWordService : IService<Word>
    {
        void AddWordAndExample(WordAndExampleDto dto);
        bool UpdateAsNewWord(AddWordDto word);
        IEnumerable<NewWord> GetNewWords();
        //IEnumerable<Word> Filter(Func<Word, bool> func, Expression<Func<Word, bool>> includeExpression);

    }
}
