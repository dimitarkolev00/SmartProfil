using System.Collections.Generic;

namespace SmartProfil.ViewModels
{
    public class CartViewModel 
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual IEnumerable<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();

        public decimal GrandTotal
        {
            get
            {
                decimal grandTotal = 0;
                foreach (var item in CartItems)
                {
                    grandTotal += item.TotalPrice;
                }

                return grandTotal;
            }
        }
    }
}
