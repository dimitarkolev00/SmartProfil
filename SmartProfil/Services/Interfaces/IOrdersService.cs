using System.Threading.Tasks;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Services.Interfaces
{
    public interface IOrdersService
    {
        Task CreateAsync(OrderFormInputModel productModel, string userId);
    }
}
