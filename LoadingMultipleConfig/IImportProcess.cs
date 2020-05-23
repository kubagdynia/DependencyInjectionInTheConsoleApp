using System.Collections.Generic;

namespace LoadingMultipleConfig
{
    public interface IImportProcess
    {
        IEnumerable<string> DoImport();
    }
}