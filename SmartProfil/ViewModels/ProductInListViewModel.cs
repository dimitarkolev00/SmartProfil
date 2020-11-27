using Microsoft.AspNetCore.Identity;

namespace SmartProfil.ViewModels
{
    public class ProductInListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public string Image { get; set; }

        public string Manufacturer { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
