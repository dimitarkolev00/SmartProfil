using System.ComponentModel.DataAnnotations;

namespace SmartProfil.ViewModels.InputModels
{
    public class AddProductMaterialTypeInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; }
    }
}
