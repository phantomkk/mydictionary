using Crawler.Web.BusinessLayer;
using MongoDb.DataAccess.Entities;
using MyDictionary.Services.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDictionary.Services.Services
{
    public interface IWordService : IService<Word>
    {
        Task AddWordAndExampleAsync(WordAndExampleDto dto);
        Task<bool> UpdateAsNewWordAsync(AddWordDto word);
        Task<IEnumerable<NewWord>> GetNewWords();
        //IEnumerable<Word> Filter(Func<Word, bool> func, Expression<Func<Word, bool>> includeExpression);

    }
}
