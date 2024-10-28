using EasyMart.Models;
using EasyMart.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyMart.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

         public ViewResult List(string category)
        {
            IEnumerable<Product> products;
            string? currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                products = _productRepository.AllProducts.OrderBy(p => p.ProductId);
                currentCategory = "All Products";
            }
            else
            {
                products = _productRepository.AllProducts.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.ProductId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new ProductListViewModel(products, currentCategory));
        }

        public IActionResult Details(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }
        public IActionResult Search()
        {
            return View();
        }
    }
}
