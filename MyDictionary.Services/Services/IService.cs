using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Crawler.Web.BusinessLayer
{
    public interface IService<T>
    {
        IList<T> GetAll();
        T GetById(int id);
        T Create(T model);
        IQueryable<T> Filter(Expression<Func<T, bool>> func); 

        bool Create(IList<T> models);
        bool Update(T model);

        bool Delete(T model);   

        bool Delete(int id);    
    }
}
