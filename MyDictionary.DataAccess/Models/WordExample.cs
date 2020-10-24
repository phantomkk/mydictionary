using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.DataAccess.Models
{
    public class WordExample
    {
        public int WordId { get; set; }
        public Word Word { get; set; }
        public int ExampleId { get; set; }
        public Example Example{get;set;}
    }
}
