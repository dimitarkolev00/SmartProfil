using System.Linq;
using AutoMapper;
using SmartProfil.AutoMapper;
using SmartProfil.Models;

namespace SmartProfil.ViewModels
{
    public class IndexPageProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public string Image { get; set; }

        public string ManufacturerName { get; set; }

        public decimal UnitPrice { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, IndexPageProductViewModel>()
                .ForMember(x => x.Image, opt =>
                    opt.MapFrom(x =>
                        "/images/products/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }

    }
}
