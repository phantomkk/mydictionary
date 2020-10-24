using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyDictionary.DataAccess;
using MyDictionary.Web.DataAccess.Context;
using System;
using System.Collections.Generic;  
using System.Linq;
using System.Linq.Expressions;
using System.Text;
namespace MyDictionary.Web.DataAccess.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _entities; 
        public DbContext Context;

        public GenericRepository(IUnitOfWork<DictionaryDbContext> unitOfWork)
        {
            Context = unitOfWork.Context;
        }

        public virtual IQueryable<T> Table
        {
            get { return Entities; }
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                return _entities ?? (_entities = Context.Set<T>());
            }
        }

        public T Create(T t)
        {
            Entities.Add(t);
            return t;
        }

        public bool Delete(T t)
        {
            Entities.Remove(t);
            return true;
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> func)
        {
            var results = Entities.Where(func);
            return results;
        }

        public IQueryable<T> Filter<TProperty>(Expression<Func<T, bool>> func, Expression<Func<T, TProperty>> eagerOptions)  
        {
            var results = Entities.Include(eagerOptions).Where(func);
            return results;
        }

        public IIncludableQueryable<T, TProperty> Querytable<TProperty>(Expression<Func<T, TProperty>> includeOption)
        {
            return Entities.Include(includeOption);
        }
         
        public IQueryable<T> Querytable() 
        {
            var results = Entities.AsQueryable();//.Where(func);
            return results;
        }

        public bool Update(T t)
        {
            Context.Entry(t).State = EntityState.Modified;
            return true;
        }

        public bool Delete(int id)
        {
            var model = Entities.Find(id);
            if(model == null)
            {
                return false;
            }

            Entities.Remove(model);
            return true;
        }

        public T First(Func<T, bool> func)
        {
            return Entities.FirstOrDefault(func);
        }
    }
}
