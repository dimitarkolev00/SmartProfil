using System.Collections.Generic;

namespace SmartProfil.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<ProductInListViewModel> Products { get; set; }

        public int PageNumber { get; set; }
    }
}
