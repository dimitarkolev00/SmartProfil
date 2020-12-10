using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SmartProfil.ViewModels.InputModels
{
    public class AddManufacturerInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Country { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
