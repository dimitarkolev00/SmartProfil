using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartProfil.Models
{
    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageSource { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
