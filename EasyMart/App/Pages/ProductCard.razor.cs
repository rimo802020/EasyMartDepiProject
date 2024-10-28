using EasyMart.Models;
using Microsoft.AspNetCore.Components;

namespace EasyMart.App.Pages
{
    public partial class ProductCard
    {
        [Parameter]
        public Product? Product { get; set; }
    }
}
