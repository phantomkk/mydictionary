using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDb.DataAccess.Entities
{
    public class Example : BaseEntity
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; } 
    }
}
