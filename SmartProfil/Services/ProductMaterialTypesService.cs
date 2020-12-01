using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartProfil.Data;
using SmartProfil.Models;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Services
{
    public class ProductMaterialTypesService : IProductMaterialTypesService
    {
        private readonly ApplicationDbContext db;

        public ProductMaterialTypesService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.db.ProductMaterialTypes.Select(x => new
            {
                x.Id,
                x.Name
            })
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public async Task AddAsync(AddProductMaterialTypeInputModel inputModel)
        {
            var productMaterial = new ProductMaterialType
            {
                Name = inputModel.Name
            };

            await this.db.ProductMaterialTypes.AddAsync(productMaterial);
            await this.db.SaveChangesAsync();
        }
    }
}
