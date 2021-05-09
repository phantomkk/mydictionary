using System.Collections.Generic;

namespace Infrastructure.Configuration
{
    [ConfigPath("DataImport")]
    public class ImportConfig
    {
        public IList<string> Paths { get; set; } = new List<string>();
    }
}