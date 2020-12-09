using System.Collections.Generic;
using System.Threading.Tasks;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateAsync(ProductInputModel productModel, string userId);

        IEnumerable<T> GetAll<T>(int page, int productsPerPage = 1);

        IEnumerable<T> GetRandom<T>(int count);

        int GetCount();

        T GetById<T>(int id);
    }
}
