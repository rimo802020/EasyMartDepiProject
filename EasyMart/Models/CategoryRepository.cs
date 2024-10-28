namespace EasyMart.Models
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly EasyMartDbContext _easyMartDbContext;

        public CategoryRepository(EasyMartDbContext easyMartDbContext)
        {
            _easyMartDbContext = easyMartDbContext;
        }

        public IEnumerable<Category> AllCategories => _easyMartDbContext.Categories.OrderBy(p => p.CategoryName);
    }
}
