using System.Collections.Generic;
using System.Linq;
using SmartProfil.Data;
using SmartProfil.Services.Interfaces;

namespace SmartProfil.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext db;

        public CategoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.db.Categories.Select(x => new
            {
                x.Id,
                x.Name
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
