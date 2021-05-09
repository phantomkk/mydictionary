using MyDictionary.Service.Mongo.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Services.Dtos
{
    public class WordExampleDto
    {
        public int WordId { get; set; }
        public WordDto Word { get; set; }
        public int ExampleId { get; set; }
        public ExampleDto Example { get; set; }
    }
}
