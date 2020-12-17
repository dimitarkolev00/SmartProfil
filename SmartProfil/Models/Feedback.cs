using System.ComponentModel.DataAnnotations;

namespace SmartProfil.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        public string Opinion { get; set; }

        public int Rating { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        
    }
}
