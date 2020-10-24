using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyDictionary.Web.DataAccess.Repositories
{
    public interface IRepository<T>
    {
        T Create(T t);

        bool Update(T t);

        bool Delete(T t);

        bool Delete(int id);

        IQueryable<T> Filter(Expression<Func<T, bool>> func);
        T First(Func<T, bool> func);

        IQueryable<T> Filter<TProperty>(Expression<Func<T, bool>> func, Expression<Func<T, TProperty>> eagerOptions); 
        
        IIncludableQueryable<T, TProperty> Querytable<TProperty>(Expression<Func<T, TProperty>> includeOption); 
        IQueryable<T> Querytable();
    }
}
