using System.Collections.Generic;

namespace SmartProfil.Services.Interfaces
{
    public interface ICategoriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
