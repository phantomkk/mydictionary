using MyDictionary.Services.Dtos;
using System.Collections.Generic;

namespace MyDictionary.Web.Models
{
    public class ListExampleViewDto
    {
        public IEnumerable<ExampleDto> Examples { get; set; }
    }
}
