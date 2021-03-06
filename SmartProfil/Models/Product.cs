﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartProfil.Models
{
    public class Product
    {
        public Product()
        {
            this.Feedbacks = new HashSet<Feedback>();
            this.Images = new HashSet<Image>();
        }
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(15)]
        public string Model { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int ProductMaterialTypeId { get; set; }

        public virtual ProductMaterialType ProductMaterialType { get; set; }

        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public string Description { get; set; }

        public string Specifications { get; set; }

        public decimal UnitPrice { get; set; }

        public int? UnitsInStock { get; set; }

        public double? Weight { get; set; }

        public double? Length { get; set; }

        public double? Width { get; set; }

        public string AddedByUserId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
