using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDb.DataAccess.Entities
{
    public class User : BaseEntity
    {
        //public int Id { get; set; }
        public string Name { get; set; }
    }
}
