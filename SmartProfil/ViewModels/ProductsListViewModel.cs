using System.Collections.Generic;

namespace SmartProfil.ViewModels
{
    public class ProductsListViewModel : PagingViewModel
    {
        public IEnumerable<ProductInListViewModel> Products { get; set; }
    }
}
