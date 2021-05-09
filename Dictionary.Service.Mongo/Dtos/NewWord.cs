using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Services.Dtos
{
    public class NewWord
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FirstExampleSection { get; set; }
        public string SecondExampleSection { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
