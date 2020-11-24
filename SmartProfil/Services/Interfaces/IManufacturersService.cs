using System.Collections.Generic;

namespace SmartProfil.Services.Interfaces
{
    public interface IManufacturersService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
