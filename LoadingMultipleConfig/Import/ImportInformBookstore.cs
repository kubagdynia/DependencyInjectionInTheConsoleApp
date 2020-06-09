using System.Collections.Generic;
using LoadingMultipleConfig.Configuration.Models;

namespace LoadingMultipleConfig.Import
{
    public class ImportInformBookstore : IImport
    {
        public List<string> Import(List<Book> books)
        {
            var resultList = new List<string> { "The bookstore will be informed of the following books:" };

            foreach (var book in books)
            {
                resultList.Add(book?.Title);
            }

            return resultList;
        }
    }
}