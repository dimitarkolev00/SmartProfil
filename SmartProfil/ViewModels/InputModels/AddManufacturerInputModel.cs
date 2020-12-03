using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SmartProfil.ViewModels.InputModels
{
    public class AddManufacturerInputModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
