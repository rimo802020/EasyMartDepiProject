namespace EasyMart.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        Product? GetProductById(int productId);
        IEnumerable<Product> SearchProducts(string searchQuery);
    }
}
