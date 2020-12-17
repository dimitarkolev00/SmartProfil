using System.Collections.Generic;
using System.Threading.Tasks;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateAsync(ProductInputModel productModel, string userId);

        IEnumerable<T> GetAll<T>(int page, int productsPerPage );

        IEnumerable<T> GetAllProfiles<T>(int page, int productsPerPage );

        IEnumerable<T> GetAllAccessories<T>(int page, int productsPerPage );

        IEnumerable<T> GetAllWindowSills<T>(int page, int productsPerPage );

        IEnumerable<T> GetRandom<T>(int count);

        int GetCount();

        int GetProfilesCount();

        int GetAccessoriesCount();

        int GetWindowSillsCount();

        T GetById<T>(int id);
    }
}
