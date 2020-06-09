using System.Collections.Generic;
using LoadingMultipleConfig.Configuration.Models;

namespace LoadingMultipleConfig.Import
{
    public interface IImport
    {
        List<string> Import(List<Book> books);
    }
}