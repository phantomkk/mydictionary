using MyDictionary.Service.Mongo.Dtos;
using MyDictionary.Services.Dtos;
using System.Collections.Generic;

namespace MyDictionary.Web.Models
{
    public class ExampleDetailDto
    {
        public ExampleDto Example { get; set; }
        public List<WordDto> NewWords { get; set; }
    }
}
