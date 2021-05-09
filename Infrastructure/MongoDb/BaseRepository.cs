using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infrastructure.Configuration;
using Infrastructure.DependencyInjection;
using Infrastructure.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.MongoDb
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collections;
        private readonly IUnitOfWork _uow;

        public BaseRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _collections = _uow.GetCollection<T>();
        }

        public IMongoCollection<T> MongoCollection => _collections;

        public async Task<T> GetByIdAsync(ObjectId id)
        {
            var filterDef = Builders<T>.Filter.Eq("_id", id);

            var documents = await _collections.FindAsync(filterDef);

            return documents.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var documents = await _collections.FindAsync(x => true);

            return documents.ToEnumerable();
        }

        public async Task<T> Create(T t)
        {
            await _collections.InsertOneAsync(t);
            var documents = await _collections.FindAsync(x => x.Id == t.Id);

            return documents.FirstOrDefault();
        }

        public async Task<IList<T>> Filter(Expression<Func<T, bool>> filter)
        {
            var document = await _collections.FindAsync(filter);

            return await document.ToListAsync();
        }

        public async Task<T> Update(T t)
        {
            var document = await _collections.ReplaceOneAsync(x => x.Id == t.Id, t);

            var documents = await _collections.FindAsync(x => x.Id == t.Id);

            return documents.FirstOrDefault();
        }

        public Task Delete(T t)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}