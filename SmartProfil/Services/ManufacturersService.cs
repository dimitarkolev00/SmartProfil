using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartProfil.Data;
using SmartProfil.Models;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Services
{
    public class ManufacturersService : IManufacturersService
    {
        private readonly ApplicationDbContext db;

        public ManufacturersService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.db.Manufacturers.Select(x => new
            {
                x.Id,
                x.Name
            })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public async Task AddAsync(AddManufacturerInputModel inputModel)
        {
            var manufacturer = new Manufacturer
            {
                Name = inputModel.Name,
                Country = inputModel.Country
            };

            await this.db.Manufacturers.AddAsync(manufacturer);
            await this.db.SaveChangesAsync();
        }
    }
}
