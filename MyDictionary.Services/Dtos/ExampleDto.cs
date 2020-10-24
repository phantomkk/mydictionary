using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Services.Dtos
{
    public class ExampleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public List<WordExampleDto> Words { get; set; }
    }
}
