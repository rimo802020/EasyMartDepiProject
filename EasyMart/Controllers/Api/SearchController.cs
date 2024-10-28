using EasyMart.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyMart.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public SearchController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var allProducts = _productRepository.AllProducts;
            return Ok(allProducts);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!_productRepository.AllProducts.Any(p => p.ProductId == id))
                return NotFound();
            return Ok(_productRepository.AllProducts.Where(p => p.ProductId == id));
        }

        [HttpPost]
        public IActionResult SearchProducts([FromBody] string searchQuery)
        {
            IEnumerable<Product> products = new List<Product>();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = _productRepository.SearchProducts(searchQuery);
            }
            return new JsonResult(products);
        }


    }
}
