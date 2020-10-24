using Microsoft.EntityFrameworkCore;
using MyDictionary.DataAccess.Models;
using MyDictionary.Web.DataAccess.Context;
using MyDictionary.Web.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyDictionary.DataAccess.Repositories
{
    public class WordExampleRepos : GenericRepository<WordExample>, IWordExampleRepos
    {
        public WordExampleRepos(IUnitOfWork<DictionaryDbContext> unitOfWork) : base(unitOfWork) { }

        public void AddListWordExample(IEnumerable<WordExample> list)
        {
            foreach (var wx in list) { Create(wx); };
        }


        public override IQueryable<WordExample> Filter(Expression<Func<WordExample, bool>> func)
        {
            return Entities
                .Include(x => x.Example)
                .Include(x => x.Word)
                .Where(func);
        }

        public IQueryable<WordExample> FilterByWordId(List<int> wordIds)
        {
            return Entities.Include(x => x.Example).Where(x => wordIds.Contains(x.WordId));
        }
    }
}
