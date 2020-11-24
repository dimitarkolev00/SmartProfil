using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmartProfil.Models.Enum;

namespace SmartProfil.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
