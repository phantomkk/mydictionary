using MyDictionary.DataAccess.Models;
using MyDictionary.Web.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDictionary.DataAccess.Repositories
{
    public interface IWordExampleRepos:IRepository<WordExample>
    {
        void AddListWordExample(IEnumerable<WordExample> list);
        IQueryable<WordExample> FilterByWordId(List<int> wordIds);
    }
}
