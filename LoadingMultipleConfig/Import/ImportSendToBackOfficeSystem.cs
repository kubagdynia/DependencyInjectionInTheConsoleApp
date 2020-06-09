using System.Collections.Generic;
using LoadingMultipleConfig.Configuration.Models;

namespace LoadingMultipleConfig.Import
{
    public class ImportSendToBackOfficeSystem : IImport
    {
        public List<string> Import(List<Book> books)
        {
            var resultList = new List<string> { "The following books have been sent to the back office system:" };

            foreach (var book in books)
            {
                resultList.Add(book?.Title);
            }

            return resultList;
        }
    }
}