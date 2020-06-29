using System.Collections.Generic;

namespace LoadingMultipleConfig.Import
{
    public interface IImport
    {
        List<string> Import();
    }
}