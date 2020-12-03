using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using SmartProfil.Data;
using SmartProfil.Models;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Services
{
    public class ManufacturersService : IManufacturersService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif", "jpeg", "tif" };

        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment webHost;

        public ManufacturersService(ApplicationDbContext db, IWebHostEnvironment webHost)
        {
            this.db = db;
            this.webHost = webHost;
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

            foreach (var image in inputModel.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                var wwwrootPath = this.webHost.WebRootPath;

                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension} !");
                }

                var dbImage = new Image
                {
                    Extension = extension
                };

                manufacturer.Images.Add(dbImage);

                var physicalPath = $"{wwwrootPath}/images/logos/{dbImage.Id}.{extension}";
                await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.db.Manufacturers.AddAsync(manufacturer);
            await this.db.SaveChangesAsync();
        }
    }
}
