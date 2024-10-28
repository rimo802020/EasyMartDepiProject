using Microsoft.EntityFrameworkCore;

namespace EasyMart.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly EasyMartDbContext _easyMartDbContext;

        public string? ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        private ShoppingCart(EasyMartDbContext easyMartDbContext)
        {
			_easyMartDbContext = easyMartDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

			EasyMartDbContext context = services.GetService<EasyMartDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Product product)
        {
            var shoppingCartItem =
					_easyMartDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };

				_easyMartDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
			_easyMartDbContext.SaveChanges();
        }

        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem =
					_easyMartDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
					_easyMartDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

			_easyMartDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??=
					   _easyMartDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Product)
                           .ToList();
        }

        public void ClearCart()
        {
            var cartItems = _easyMartDbContext
				.ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

			_easyMartDbContext.ShoppingCartItems.RemoveRange(cartItems);

			_easyMartDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _easyMartDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Amount).Sum();
            return total;
        }
    }
}
