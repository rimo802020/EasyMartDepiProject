namespace EasyMart.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EasyMartDbContext _easyMartDbContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(EasyMartDbContext easyMartDbContext, IShoppingCart shoppingCart)
        {
			_easyMartDbContext = easyMartDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    ProductId = shoppingCartItem.Product.ProductId,
                    Price = shoppingCartItem.Product.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

			_easyMartDbContext.Orders.Add(order);

			_easyMartDbContext.SaveChanges();
        }
    }
}
