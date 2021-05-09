using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyDictionary.Services.Services;
using MongoDb.DataAccess.Entities;
using Infrastructure.MongoDb;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MyDictionary.Service.Mongo.Services
{
    public class ExampleService : GenericService<Example>, IExampleService
    {

        public ExampleService(IUnitOfWork uow) : base(uow)
        {
        }

        public override async Task<IList<Example>> Filter(Expression<Func<Example, bool>> func)
        {
            return await _mainRepos.Filter(func);
        }

        public override async Task<Example> GetById(string id)
        {
            return await _mainRepos.GetByIdAsync(ObjectId.Parse(id));
        }


    }
}
