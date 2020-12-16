using System.Data;

namespace SmartProfil.ViewModels
{
    public class ProductCartViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public string ManufacturerName { get; set; }

        public int Quantity { get; set; }

        public decimal SinglePrice { get; set; }

        public decimal TotalPrice => this.SinglePrice * this.Quantity;

        public string Image { get; set; }
    }
}
