using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Services.Interfaces
{
    public interface IProductService
    { 
        Task CreateAsync(ProductInputModel productModel, string userId);
    }
}
