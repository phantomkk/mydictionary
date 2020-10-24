using Crawler.Web.BusinessLayer;
using MyDictionary.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyDictionary.Services.Services
{
    public interface IWordExampleService:IService<WordExample>
    {
        IEnumerable<WordExample> FilterByExampleId(int exampleId);
        IQueryable<WordExample> FilterByExampleIds(Expression<Func<WordExample, bool>> func);
        IEnumerable<WordExample> FilterByWordId(int wordId);
    }
}
