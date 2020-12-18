using System.ComponentModel.DataAnnotations;

namespace SmartProfil.Models
{
    public class OrderFormInfo
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(70)]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(70)]
        public string Address { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string City { get; set; }

        [MaxLength(40)]
        public string CompanyName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string NameOnCard { get; set; }

        [CreditCard]
        public string CardNumber { get; set; }

        [Required]
        [Range(1, 12)]
        public string ExpMonth { get; set; }

        [Required]
        [Range(2020, 2040)]

        public string ExpYear { get; set; }

        [Required]
        [Range(100, 999)]
        public string CVV { get; set; }
    }
}
