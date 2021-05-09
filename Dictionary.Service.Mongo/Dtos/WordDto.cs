using MyDictionary.Services.Dtos;
using System;
using System.Collections.Generic;

namespace MyDictionary.Service.Mongo.Dtos
{
    public class WordDto
    {
        public string Id { get; set; }
        // public int Id { get; set; }
        public string Name { get; set; }
        public string Meaning { get; set; }
        public List<string> ExamplesIds { get; set; }
        public int Occurrences { get; set; }
        public bool IsNew { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
