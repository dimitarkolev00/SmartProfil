using System.ComponentModel.DataAnnotations;

namespace SmartProfil.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [Phone]
        public string PhoneNo { get; set; }
        public string CompanyName { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(150)]
        public string AddressLine { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Country { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string City { get; set; }
        
        public string ZipCode { get; set; }
    }
}
