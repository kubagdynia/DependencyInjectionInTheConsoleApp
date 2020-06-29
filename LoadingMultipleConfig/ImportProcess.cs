using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Features.Indexed;
using LoadingMultipleConfig.Configuration;
using LoadingMultipleConfig.Configuration.Models;
using LoadingMultipleConfig.Import;

namespace LoadingMultipleConfig
{
    public class ImportProcess : IImportProcess
    {
        private readonly AppConfiguration _config;
        private readonly Func<List<Book>, IImport> _imports;

        public ImportProcess(AppConfiguration config,  IIndex<ImportType, Func<List<Book>, IImport>> imports)
        {
            _config = config;
            _imports  = imports[config.Config.ImportType];
        }

        public IEnumerable<string> DoImport()
        {
            if (_config?.Config?.Books is null || !_config.Config.Books.Any())
            {
                return new List<string> { "No data to import!" };
            }

            List<string> resultList = _imports(_config.Config.Books).Import();

            return resultList;
        }
    }
}