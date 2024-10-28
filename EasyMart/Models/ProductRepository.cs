using Microsoft.EntityFrameworkCore;

namespace EasyMart.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly EasyMartDbContext _easyMartDbContext;

        public ProductRepository(EasyMartDbContext easyMartDbContext)
        {
			_easyMartDbContext = easyMartDbContext;
        }

        public IEnumerable<Product> AllProducts
        {
            get
            {
                return _easyMartDbContext.Products.Include(c => c.Category);
            }
        }

        public Product? GetProductById(int productId)
        {
            return _easyMartDbContext.Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public IEnumerable<Product> SearchProducts(string searchQuery)
        {
            return _easyMartDbContext.Products.Where(p => p.Name.Contains(searchQuery));
        }
    }
}
