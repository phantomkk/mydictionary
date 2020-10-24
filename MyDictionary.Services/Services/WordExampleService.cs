using Microsoft.EntityFrameworkCore;
using MyDictionary.DataAccess;
using MyDictionary.DataAccess.Models;
using MyDictionary.DataAccess.Repositories;
using MyDictionary.Web.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyDictionary.Services.Services
{
    public class WordExampleService: IWordExampleService
    {
        private IUnitOfWork<DictionaryDbContext> _unitOfWork;
        private IWordExampleRepos _mainRepos;
        public WordExampleService(IUnitOfWork<DictionaryDbContext> unitOfWork, IWordExampleRepos repos) 
        {
            _unitOfWork = unitOfWork;
            _mainRepos = repos;
        }

        public WordExample Create(WordExample model)
        {
            throw new NotImplementedException();
        }

        public bool Create(IList<WordExample> models)
        {
            throw new NotImplementedException();
        }

        public bool Delete(WordExample model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WordExample> Filter(Expression<Func<WordExample, bool>> func)
        {
            var query = _mainRepos
                .Querytable(x => x.Word).Include(x=>x.Example)
                .Where(func);

            return query;
        }

        public IEnumerable<WordExample> FilterByExampleId(int exampleId)
        {
            return _mainRepos.Querytable(x => x.Word).Where(x => x.ExampleId == exampleId);
        }

          public IQueryable<WordExample> FilterByExampleIds(Expression<Func<WordExample, bool>> func)
        {
            var res =  _mainRepos.Querytable(x=>x.Word).Where(func);
            return res;
        }

        public IEnumerable<WordExample> FilterByWordId(int wordId)
        {
            throw new NotImplementedException();
        }

        public IList<WordExample> GetAll()
        {
            throw new NotImplementedException();
        }

        public WordExample GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(WordExample model)
        {
            throw new NotImplementedException();
        }
    }
}
