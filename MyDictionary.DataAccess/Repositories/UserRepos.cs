using MyDictionary.DataAccess.Models;
using MyDictionary.Web.DataAccess.Context;
using MyDictionary.Web.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.DataAccess.Repositories
{
    public class UserRepos:GenericRepository<User>, IUserRepos
    {

        public UserRepos(IUnitOfWork<DictionaryDbContext> unitOfWork) : base(unitOfWork) { }
    }
}
