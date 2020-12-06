using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SmartProfil.AutoMapper;
using SmartProfil.Models;

namespace SmartProfil.ViewModels
{
    public class SingleProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Model { get; set; }

        public string Image { get; set; }

        public string CategoryName { get; set; }

        public string ProductMaterialTypeName { get; set; }

        public string ManufacturerName { get; set; }

        public string ManufacturerImage { get; set; }

        public string Description { get; set; }

        public string Specifications { get; set; }

        public decimal UnitPrice { get; set; }

        public int? UnitsInStock { get; set; }

        public double? Weight { get; set; }

        public double? Length { get; set; }

        public double? Width { get; set; }

        //public virtual ICollection<Feedback> Feedbacks { get; set; }

        //public virtual ICollection<Image> Images { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, SingleProductViewModel>()
                .ForMember(x => x.Image, opt =>
                    opt.MapFrom(x =>
                        "/images/products/" + x.Images.FirstOrDefault().Id + "." +
                        x.Images.FirstOrDefault().Extension))
                .ForMember(x => x.ManufacturerImage, opt =>
                    opt.MapFrom(x =>
                        "/images/logos/" + x.Manufacturer.Images.FirstOrDefault().Id + "." +
                        x.Manufacturer.Images.FirstOrDefault().Extension));

        }
    }
}
