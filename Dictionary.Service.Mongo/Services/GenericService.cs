using Crawler.Web.BusinessLayer;
using Infrastructure.Entities;
using Infrastructure.MongoDb;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDictionary.Services.Services
{
    public class GenericService<T> : IService<T> where T : BaseEntity
    {
        private IUnitOfWork _uow;
        protected IBaseRepository<T> _mainRepos;
        public GenericService(IUnitOfWork uow)
        {
            _uow = uow;
            _mainRepos = uow.CreateRepo<T>();
        }

        public virtual async Task<IList<T>> Filter(Expression<Func<T, bool>> func)
        {
            return await _mainRepos.Filter(func);
        }

        public async Task<IList<T>> GetAll()
        {

            return await _mainRepos.Filter(x => true);
        }



        public async Task<bool> Create(IList<T> models)
        {
            foreach (var model in models)
            {
                await _mainRepos.Create(model);
            }

            return true;
        }

        public async Task<bool> Update(T model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(T model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Create(T model)
        {
            await _mainRepos.Create(model);
            return model;
        }

        public virtual async Task<T> GetById(string id)
        {
            return await _mainRepos.GetByIdAsync(ObjectId.Parse(id));
        }

        public virtual async Task<T> GetById(ObjectId id)
        {
            return await _mainRepos.GetByIdAsync(id);
        } 
    }
}
