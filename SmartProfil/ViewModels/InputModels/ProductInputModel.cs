using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SmartProfil.ViewModels.InputModels
{
    public class ProductInputModel 
    {
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Model { get; set; }

        public int CategoryId { get; set; }

        public int ProductMaterialTypeId { get; set; }

        public int ManufacturerId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(2000)]
        public string Description { get; set; }
        
        public string Specifications { get; set; }

        [Range(0, Int32.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Range(0, Int32.MaxValue)]
        public int? UnitsInStock { get; set; }

        [Range(0, Int32.MaxValue)]
        public double? Weight { get; set; }

        [Range(0, Int32.MaxValue)]
        public double? Length { get; set; }

        [Range(0, Int32.MaxValue)]
        public double? Width { get; set; }

        [Required]
        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Manufacturers { get; set; }

        public IEnumerable<KeyValuePair<string, string>> ProductMaterialTypes { get; set; }
    }
}
