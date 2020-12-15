using System.ComponentModel.DataAnnotations;

namespace SmartProfil.ViewModels.InputModels
{
    public class AddToCartInputModel
    {
        [Required]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
