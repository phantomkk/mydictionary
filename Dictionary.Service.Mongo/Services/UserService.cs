using Infrastructure.MongoDb;
using MongoDb.DataAccess.Entities;

namespace MyDictionary.Services.Services
{
    public class UserService:GenericService<User>, IUserService
    {
        public UserService(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
