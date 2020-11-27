using System.Collections.Generic;
using System.Threading.Tasks;
using SmartProfil.ViewModels;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateAsync(ProductInputModel productModel, string userId);

        IEnumerable<ProductInListViewModel>GetAll(int page, int productsPerPage = 12);
    }
}
