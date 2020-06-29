using System.Collections.Generic;
using LoadingMultipleConfig.Configuration.Models;

namespace LoadingMultipleConfig.Import
{
    public class ImportSendToBackOfficeSystem : IImport
    {
        private readonly List<Book> _books;
        public ImportSendToBackOfficeSystem(List<Book> books)
        {
            _books = books;
        }
        public List<string> Import()
        {
            var resultList = new List<string> { "The following books have been sent to the back office system:" };

            foreach (var book in _books)
            {
                resultList.Add(book?.Title);
            }

            return resultList;
        }
    }
}