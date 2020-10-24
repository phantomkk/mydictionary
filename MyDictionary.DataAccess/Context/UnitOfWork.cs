
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace MyDictionary.Web.DataAccess.Context
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable where
        TContext : DbContext 
    {
        private bool _disposed;
        private string _errorMessage = string.Empty;
        private IDbContextTransaction _objTran;

        public UnitOfWork(TContext context)
        {
            Context = context;
        }

        public TContext Context { get; }

        public void BeginTransaction()
        {
            _objTran = Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _objTran.Commit();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        public void Rollback()
        {
            _objTran.Rollback();
            _objTran.Dispose();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        { 
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                    _disposed = true;
                }
            }
        }
    }
}
