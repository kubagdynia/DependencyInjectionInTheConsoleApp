using System.Collections.Generic;
using LoadingMultipleConfig.Configuration.Models;

namespace LoadingMultipleConfig.Import
{
    public class ImportInformBookstore : IImport
    {
        private readonly List<Book> _books;

        public ImportInformBookstore(List<Book> books)
        {
            _books = books;
        }
        
        public List<string> Import()
        {
            var resultList = new List<string> { "The bookstore will be informed of the following books:" };

            foreach (var book in _books)
            {
                resultList.Add(book?.Title);
            }

            return resultList;
        }
    }
}