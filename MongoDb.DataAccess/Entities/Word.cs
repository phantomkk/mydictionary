using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDb.DataAccess.Entities
{
    public class Word : BaseEntity
    {
        public string Meaning { get; set; }
        public List<string> ExampleIds{ get; set; }
        public int Occurrences { get; set; }
        public bool IsNew { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

    }
}
