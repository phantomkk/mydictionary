using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Services.Dtos
{
    public class WordDto
    {
        public int Id { get; set; }
        // public int Id { get; set; }
        public string Name { get; set; }
        public string Meaning { get; set; }
        public List<WordExampleDto> Examples { get; set; }
        public int Occurrences { get; set; }
        public bool IsNew { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
