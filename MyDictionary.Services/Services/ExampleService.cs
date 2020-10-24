using System;
using MyDictionary.DataAccess;
using MyDictionary.DataAccess.Models;
using MyDictionary.DataAccess.Repositories;
using MyDictionary.Web.DataAccess.Context;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MyDictionary.Services.Services
{
    public class ExampleService : GenericService<Example>, IExampleService
    {

        public ExampleService(IUnitOfWork<DictionaryDbContext> unitOfWork, IExampleRepos repos) : base(unitOfWork, repos)
        {
        }

        public override IQueryable<Example> Filter(Expression<Func<Example, bool>> func)
        {
            return _mainRepos.Filter(func);
        }

        public override Example GetById(int id)
        {
            return _mainRepos.First(x => x.Id == id);
        }


    }
}
