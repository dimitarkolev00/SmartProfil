using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SmartProfil.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Feedbacks = new HashSet<Feedback>();
        }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
