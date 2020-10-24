using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.DataAccess.Models
{
    public class Word : BaseModel
    {
       // public int Id { get; set; }
        public string Name { get; set; }
        public string Meaning { get; set; }
        public List<WordExample> Examples { get; set; }
        public int Occurrences { get; set; }
        public bool IsNew { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

    }
}
