using System.Collections.Generic;

namespace Macerus.ContentConverter
{
    public interface IStringResourceContentConverter
    {
        void WriteStringResourceModule(string namespaceForModule, string moduleClassName, IEnumerable<KeyValuePair<string, string>> resourceKvps, string outputFilePath);
    }
}