using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartProfil.Models
{
    public class Cart
    {
        public Cart()
        {
            this.CartItems = new HashSet<CartItem>();
        }
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }

    }
}
