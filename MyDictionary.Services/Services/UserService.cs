using MyDictionary.DataAccess;
using MyDictionary.DataAccess.Models;
using MyDictionary.DataAccess.Repositories;
using MyDictionary.Web.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.Services.Services
{
    public class UserService:GenericService<User>, IUserService
    {
        public UserService(IUnitOfWork<DictionaryDbContext> unitOfWork, IUserRepos repos) : base(unitOfWork, repos)
        {
        }
    }
}
