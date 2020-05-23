using System.Collections.Generic;
using System.Linq;
using LoadingMultipleConfig.Configuration;
using LoadingMultipleConfig.Configuration.Models;

namespace LoadingMultipleConfig
{
    public class ImportProcess : IImportProcess
    {
        private readonly AppConfiguration _config;

        public ImportProcess(AppConfiguration config)
            => _config = config;

        public IEnumerable<string> DoImport()
        {
            if (_config?.Config?.Books is null || !_config.Config.Books.Any())
            {
                return new List<string> { "No data to import!" };
            }

            string taskToDo = _config.Config.ImportType switch
            {
                ImportType.InformBookstore => "The bookstore will be informed of the following books:",
                ImportType.SaveInDb => "The following books have been saved in the database:",
                ImportType.SendToBackOfficeSystem => "The following books have been sent to the back office system:",
                _ => ":("
            };

            var resultList = new List<string> { taskToDo };

            foreach (var book in _config.Config.Books)
            {
                resultList.Add(book?.Title);
            }

            return resultList;
        }
    }
}