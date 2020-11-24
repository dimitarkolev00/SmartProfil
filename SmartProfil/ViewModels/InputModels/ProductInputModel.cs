using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;

namespace SmartProfil.ViewModels.InputModels
{
    public class ProductInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string Model { get; set; }

        public int CategoryId { get; set; }

        public int ProductMaterialTypeId { get; set; }

        public int ManufacturerId { get; set; }

        public string Description { get; set; }

        public string Specifications { get; set; }

        public string ImageSource { get; set; }

        public decimal UnitPrice { get; set; }

        public int? UnitsInStock { get; set; }

        public double? Weight { get; set; }

        public double? Length { get; set; }

        public double? Width { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Manufacturers { get; set; }
        public IEnumerable<KeyValuePair<string, string>> ProductMaterialTypes { get; set; }
    }
}
