using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SmartProfil.Models
{
    public class Cart
    {
        public Cart()
        {
            this.CartItems = new HashSet<CartItem>();
        }
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }


        public void AddItem(int productId, int quantity = 1, decimal unitPrice = 1)
        {
            var existingItem = CartItems.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
                existingItem.TotalPrice = existingItem.Quantity * existingItem.UnitPrice;
            }
            else
            {
                CartItems.Add(
                    new CartItem
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        UnitPrice = unitPrice,
                        TotalPrice = quantity * unitPrice
                    });
            }
        }

        public void RemoveItem(int cartItemId)
        {
            var removedItem = CartItems.FirstOrDefault(x => x.Id == cartItemId);
            if (removedItem != null)
            {
                CartItems.Remove(removedItem);
            }
        }

        public void RemoveItemWithProduct(int productId)
        {
            var removedItem = CartItems.FirstOrDefault(x => x.ProductId == productId);
            if (removedItem != null)
            {
                CartItems.Remove(removedItem);
            }
        }

        public void ClearItems()
        {
            CartItems.Clear();
        }
    }
}
