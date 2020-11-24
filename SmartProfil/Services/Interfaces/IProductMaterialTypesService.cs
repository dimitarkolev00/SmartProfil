using System.Collections.Generic;

namespace SmartProfil.Services.Interfaces
{
    public interface IProductMaterialTypesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
