using System.Collections.Generic;
using System.Linq;
using SmartProfil.Data;
using SmartProfil.Services.Interfaces;

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
    }
}
