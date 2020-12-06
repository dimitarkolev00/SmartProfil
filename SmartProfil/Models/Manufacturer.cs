using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartProfil.Models
{
    public class Manufacturer
    {
        public Manufacturer()
        {
            this.Products = new HashSet<Product>();
            this.Images = new HashSet<Image>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
