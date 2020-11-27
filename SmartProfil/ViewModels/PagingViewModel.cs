using System;

namespace SmartProfil.ViewModels
{
    public class PagingViewModel
    {
        public int PageNumber { get; set; }

        public int ProductsCount { get; set; }

        public int ItemsPerPage { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.ProductsCount / ItemsPerPage);
    }
}
