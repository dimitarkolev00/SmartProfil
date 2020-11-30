using System.Collections.Generic;
using SmartProfil.Models;

namespace SmartProfil.Services.Interfaces
{
    public interface ICategoriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

    }
}
