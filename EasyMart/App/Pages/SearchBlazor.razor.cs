using EasyMart.Models;
using Microsoft.AspNetCore.Components;

namespace EasyMart.App.Pages
{
    public partial class SearchBlazor
    {
        public string SearchText = "";
        public List<Product> FilteredProducts { get; set; } = new List<Product>();

        [Inject]
        public IProductRepository? ProductRepository { get; set; }

        private void Search()
        {
            FilteredProducts.Clear();
            if (ProductRepository is not null)
            {
                if (SearchText.Length >= 3)
                    FilteredProducts = ProductRepository.SearchProducts(SearchText).ToList();
            }
        }
    }
}
