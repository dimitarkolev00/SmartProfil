using SmartProfil.AutoMapper;
using SmartProfil.Models;

namespace SmartProfil.ViewModels
{
    public class CartItemViewModel : IMapFrom<CartItem>
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public int ProductId { get; set; }

        public SingleProductViewModel Product { get; set; }
    }
}
