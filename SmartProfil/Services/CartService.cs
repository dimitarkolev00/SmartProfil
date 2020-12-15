using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartProfil.Data;
using SmartProfil.Models;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels;

namespace SmartProfil.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext db;

        public CartService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddToCartAsync(int productId, string userId, int quantity = 1)
        {

            await this.db.ProductCarts.AddAsync(new ProductCart
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity

            });

            await this.db.SaveChangesAsync();
        }

        public List<ProductCartViewModel> GetAllProductsForCartViewModel(string userId)
        {
            return this.db.ProductCarts
                .Where(x => x.UserId == userId)
                .Select(x => new ProductCartViewModel
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Model = x.Product.Model,
                    ManufacturerName = x.Product.Manufacturer.Name,
                    Quantity = x.Quantity,
                    SinglePrice = x.Product.UnitPrice
                    
                }).ToList();
        }

        //public bool ProductIsInCart(int productId, string userId)
        //{
        //    return this.db.ProductCarts.Any(x => x.ProductId == productId && x.UserId == userId);
        //}

        public ProductCart GetProductFromCart(int productId, string userId)
        {
            return this.db.ProductCarts.FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);
        }


        //public async Task<Cart> GetCartByUserId(string userId)
        //{
        //    var cart = await GetExistingOrCreateNewCart(userId);
        //    //var cartModel = ObjectMapper.Mapper.Map<CartModel>(cart);

        //    //// If product can not loaded from razor page, we apply manuel mapping.
        //    //if (cart.CartItems.Any(c => c.Product == null))
        //    //{
        //    //    cartModel.Items.Clear();
        //    //    foreach (var item in cart.CartItems)
        //    //    {
        //    //        //var cartItemModel = ObjectMapper.Mapper.Map<CartItemModel>(item);
        //    //        var product = await _productRepository.GetProductByIdWithCategoryAsync(item.ProductId);
        //    //        var productModel = ObjectMapper.Mapper.Map<ProductModel>(product);
        //    //        cartItemModel.Product = productModel;
        //    //        cartModel.Items.Add(cartItemModel);
        //    //    }
        //    //}
        //    return cart;
        //}

        //    public async Task AddItem(string userId, int productId, int quantity)
        //    {
        //        var cart = await GetExistingOrCreateNewCart(userId);

        //        var product = this.db.Products.FirstOrDefault(x => x.Id == productId);

        //        if (product == null)
        //        {
        //            return;
        //        }

        //        var cartItem = new CartItem
        //        {
        //            ProductId = productId,
        //            Quantity = quantity,
        //            Product = product,
        //            UnitPrice = product.UnitPrice,
        //            TotalPrice = product.UnitPrice * quantity,
        //        };

        //        await this.db.CartItems.AddAsync(cartItem);
        //        await this.db.SaveChangesAsync();
        //    }

        //    public async Task RemoveItem(int cartId, int cartItemId)
        //    {
        //        var cart = this.db.Carts.FirstOrDefault(x => x.Id == cartId);

        //        if (cart==null)
        //        {
        //            return;
        //        }

        //        cart.RemoveItem(cartItemId); 

        //        this.db.Carts.Update(cart);
        //        await this.db.SaveChangesAsync();
        //    }

        //    private async Task<Cart> GetExistingOrCreateNewCart(string userId)
        //    {
        //        var cart = this.db.Carts.FirstOrDefault(x=>x.UserId == userId);

        //        if (cart != null)
        //        {
        //            return cart;
        //        }

        //        var newCart = new Cart
        //        {
        //            UserId = userId
        //        };

        //        await this.db.Carts.AddAsync(newCart);
        //        await this.db.SaveChangesAsync();

        //        return newCart;
        //    }

        //    public async Task ClearCart(string userId)
        //    {
        //        var cart = this.db.Carts.FirstOrDefault(x=>x.UserId == userId);

        //        if (cart == null)
        //        {
        //            throw new ApplicationException("Submitted order should have cart");
        //        }

        //        cart.ClearItems();

        //        this.db.Carts.Update(cart);
        //        await this.db.SaveChangesAsync();
        //    }

        //    public ICollection<CartItem> GetCartItems(int cartId)
        //    {
        //        var cart = this.db.Carts.FirstOrDefault(x => x.Id == cartId);

        //        return cart.CartItems;
        //    }
        //
    }
}
