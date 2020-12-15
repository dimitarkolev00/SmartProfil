using System.ComponentModel.DataAnnotations;

namespace SmartProfil.Models
{
    public class ProductCart
    {
        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
