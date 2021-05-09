using System.Collections.Generic;
using Infrastructure.Entities;

namespace UserService.Entities
{
    public class User: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OktaId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}