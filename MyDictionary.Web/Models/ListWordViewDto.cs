using MyDictionary.DataAccess.Models;
using MyDictionary.Services.Dtos; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Web.Models
{
    public class ListWordViewDto
    {
        public List<NewWord> Words { get; set; }
    }
}
