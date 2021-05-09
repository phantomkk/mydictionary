using System;
using System.Linq;
using Infrastructure.DataImport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Configuration
{
    public class StartupImportService : IStartupFilter
    {
        private readonly IDataImportService _importService;
        private readonly ImportConfig _importConfig;

        public StartupImportService(IDataImportService importService, ImportConfig importConfig)
        {
            _importService = importService;
            _importConfig = importConfig;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            if (_importConfig.Paths == null || !_importConfig.Paths.Any())
            {
                return next;
            }
            
            foreach (var importConfigPath in _importConfig.Paths)
            {
                _importService.Import(importConfigPath);
            }

            return next;
        }
    }
}