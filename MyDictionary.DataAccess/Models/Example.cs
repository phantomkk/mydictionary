using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.DataAccess.Models
{
    public class Example:BaseModel
    {
       // public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public List<WordExample> Words { get; set; }

    }
}
