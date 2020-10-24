using Crawler.Web.BusinessLayer;
using MyDictionary.DataAccess;
using MyDictionary.DataAccess.Models;
using MyDictionary.Web.DataAccess.Context;
using MyDictionary.Web.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyDictionary.Services.Services
{
    public class GenericService<T> : IService<T> where T : BaseModel
    {
        protected IRepository<T> _mainRepos;
        protected IUnitOfWork<DictionaryDbContext> _unitOfWork; 
        public GenericService(IUnitOfWork<DictionaryDbContext> unitOfWork,IRepository<T> repos)
        {
            _unitOfWork = unitOfWork;
            _mainRepos = repos;
        } 

        public T Create(T model)
        {
            _mainRepos.Create(model);
            _unitOfWork.Save();
            return model;
        }
        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> func)
        {
            return _mainRepos.Filter(func);
        } 
        public bool Create(IList<T> models)
        {
            foreach (var model in models)
            {
                _mainRepos.Create(model);
            }
            _unitOfWork.Save();
            return true;
        }

        public bool Delete(T model)
        {
            _mainRepos.Delete(model);
            _unitOfWork.Save();
            return true;
        }

        public bool Delete(int id)
        {
            var result = _mainRepos.Delete(id);
            _unitOfWork.Save();
            return result;
        }

        public IList<T> GetAll()
        {
            return _mainRepos.Filter(x => true).ToList();
        }

        public virtual T GetById(int id)
        {
            return _mainRepos.Filter(x => x.Id == id).FirstOrDefault();
        } 

        public bool Update(T model)
        {
            var result = _mainRepos.Update(model);
            _unitOfWork.Save();
            return result;
        }
    }
}
