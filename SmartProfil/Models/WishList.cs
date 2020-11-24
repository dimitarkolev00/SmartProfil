using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartProfil.Models
{
    public class WishList
    {
        public WishList()
        {
            this.Products = new HashSet<Product>();
        }
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
