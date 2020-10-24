using Microsoft.EntityFrameworkCore;
using System;

namespace MyDictionary.Web.DataAccess.Context
{
    public interface IUnitOfWork<out TContext> : IDisposable where TContext : DbContext
    {
        TContext Context { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}