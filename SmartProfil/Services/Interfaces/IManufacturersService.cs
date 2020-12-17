using System.Collections.Generic;
using System.Threading.Tasks;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Services.Interfaces
{
    public interface IManufacturersService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
        Task AddAsync(AddManufacturerInputModel inputModel);
    } 
}
