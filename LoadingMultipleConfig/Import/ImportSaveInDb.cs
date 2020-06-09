using System.Collections.Generic;
using LoadingMultipleConfig.Configuration.Models;

namespace LoadingMultipleConfig.Import
{
    public class ImportSaveInDb : IImport
    {
        public List<string> Import(List<Book> books)
        {
            var resultList = new List<string> { "The following books have been saved in the database:" };

            foreach (var book in books)
            {
                resultList.Add(book?.Title);
            }

            return resultList;
        }
    }
}