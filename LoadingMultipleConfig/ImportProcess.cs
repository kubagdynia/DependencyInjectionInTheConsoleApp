using System.Collections.Generic;
using System.Linq;
using LoadingMultipleConfig.Configuration;
using LoadingMultipleConfig.Import;

namespace LoadingMultipleConfig
{
    public class ImportProcess : IImportProcess
    {
        private readonly AppConfiguration _config;
        private readonly IImport _import;

        public ImportProcess(AppConfiguration config, IImport import)
        {
            _config = config;
            _import = import;
        }

        public IEnumerable<string> DoImport()
        {
            if (_config?.Config?.Books is null || !_config.Config.Books.Any())
            {
                return new List<string> { "No data to import!" };
            }

            List<string> resultList =  _import.Import(_config.Config.Books);

            return resultList;
        }
    }
}