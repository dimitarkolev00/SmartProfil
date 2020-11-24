using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartProfil.Models
{
    public class ProductMaterialType
    {
        public ProductMaterialType()
        {
            this.Products = new HashSet<Product>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
