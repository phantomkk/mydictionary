using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infrastructure.Entities;
using MongoDB.Bson;

namespace Infrastructure.MongoDb
{
    public interface IBaseRepository<T> : IDisposable where T : BaseEntity
    {
        Task< T> GetByIdAsync(ObjectId id);

        Task<IEnumerable<T>> GetAllAsync();
        
        Task<T> Create(T t);
        Task<T> Update(T t);
        Task Delete(T t);
        
        Task<IList<T>> Filter(Expression<Func<T, bool>> filter);
    }
}