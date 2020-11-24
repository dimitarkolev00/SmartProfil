using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Services.Interfaces
{
    public interface IProductService
    {
        public void Add(ProductInputModel productModel);
    }
}
