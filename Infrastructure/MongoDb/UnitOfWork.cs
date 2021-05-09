using Infrastructure.DependencyInjection;
using Infrastructure.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb
{
    public interface IUnitOfWork : IDisposable
    {
        IMongoCollection<T> GetCollection<T>();

        IClientSessionHandle BeginTransaction();

        IBaseRepository<T> CreateRepo<T>() where T : BaseEntity;

        Task CommitTransaction();


        Task AbortTransaction();

    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoDbContext _context;
        private IClientSessionHandle _session; 
        private List<object> Repositories = new List<object>();

        public UnitOfWork(IServiceProvider serviceProvider)
        { 
            _context = (IMongoDbContext)serviceProvider.GetService(typeof(IMongoDbContext));

        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _context.GetDatabase().GetCollection<T>(typeof(T).Name);
        }

        public IClientSessionHandle BeginTransaction()
        {
            _session = _context.GetMongoClient().StartSession();
            _session.StartTransaction();

            return _session;
        }

        public async Task CommitTransaction()
        {
            await _session.CommitTransactionAsync();
        }

        public async Task AbortTransaction()
        {
            await _session.AbortTransactionAsync();
        }

        public void Dispose()
        {
            if (_session != null)
            {
                _session.Dispose();
            }
        }

        public IBaseRepository<T> CreateRepo<T>() where T : BaseEntity
        {
            var existedInstance = Repositories.FirstOrDefault(x => x is BaseRepository<T>);
            if(existedInstance != null)
            {
                return (BaseRepository<T>)existedInstance;
            }

            var instance =  new BaseRepository<T>(this);
            Repositories.Add(instance);

            return instance;
        }
    }
}
