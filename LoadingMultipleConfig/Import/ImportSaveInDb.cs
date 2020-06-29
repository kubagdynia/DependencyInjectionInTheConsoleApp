using System.Collections.Generic;
using LoadingMultipleConfig.Configuration.Models;

namespace LoadingMultipleConfig.Import
{
    public class ImportSaveInDb : IImport
    {
        private readonly List<Book> _books;
        public ImportSaveInDb(List<Book> books)
        {
            _books = books;
        }
        
        public List<string> Import()
        {
            var resultList = new List<string> { "The following books have been saved in the database:" };

            foreach (var book in _books)
            {
                resultList.Add(book?.Title);
            }

            return resultList;
        }
    }
}