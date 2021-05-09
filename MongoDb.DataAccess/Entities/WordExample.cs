using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDb.DataAccess.Entities
{
    public class WordExample
    {
        public int WordId { get; set; }
        public Word Word { get; set; }
        public int ExampleId { get; set; }
        public Example Example { get; set; }
    }
}
