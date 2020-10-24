using MyDictionary.DataAccess.Models;
using MyDictionary.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Web.Models
{
    public class ExampleDetailDto
    {
        public ExampleDto Example { get; set; }
        public List<WordDto> NewWords { get; set; }
    }
}
