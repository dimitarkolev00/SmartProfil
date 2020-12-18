using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SmartProfil.Data;
using SmartProfil.Models;
using SmartProfil.Services.Interfaces;
using SmartProfil.ViewModels.InputModels;

namespace SmartProfil.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext db;

        public OrdersService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task CreateAsync(OrderFormInputModel productModel, string userId)
        {
            var orderForm = new OrderFormInfo
            {
                FullName = productModel.FullName,
                Email = productModel.Email,
                Address= productModel.Address,
                CardNumber = ComputeHash(productModel.CardNumber),
                City= productModel.City,
                CompanyName = productModel.CompanyName,
                CVV= ComputeHash(productModel.CVV),
                ExpMonth= productModel.ExpMonth,
                ExpYear= productModel.ExpYear,
                NameOnCard = productModel.NameOnCard,
                UserId = userId
            };

            await this.db.OrderFormInfo.AddAsync(orderForm);
            await this.db.SaveChangesAsync();
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
