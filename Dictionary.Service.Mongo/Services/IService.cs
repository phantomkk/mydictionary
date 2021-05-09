using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Web.BusinessLayer
{
    public interface IService<T>
    {
         Task<IList<T>> GetAll();
         Task<T> GetById(string id);
         Task<T> GetById(ObjectId id);
         Task<T> Create(T model);
         Task<IList<T>> Filter(Expression<Func<T, bool>> func);

         Task<bool> Create(IList<T> models);
         Task<bool> Update(T model);

         Task<bool> Delete(T model);   

         Task<bool> Delete(int id);    
    }
}
