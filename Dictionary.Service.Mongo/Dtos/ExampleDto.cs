using MyDictionary.Service.Mongo.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Services.Dtos
{
    public class ExampleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public List<WordDto> Words { get; set; }
    }
}
