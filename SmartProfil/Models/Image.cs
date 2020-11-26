using System;

namespace SmartProfil.Models
{
    public class Image
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }
    }
}
