using EasyMart.Models;
using EasyMart.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class ShoppingCartSummary : ViewComponent
{
    private readonly IShoppingCart _shoppingCart;

    public ShoppingCartSummary(IShoppingCart shoppingCart)
    {
        _shoppingCart = shoppingCart;
    }

    public IViewComponentResult Invoke()
    {
        var items = _shoppingCart.GetShoppingCartItems(); 

        var total = _shoppingCart.GetShoppingCartTotal();
        var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, total);

        return View(shoppingCartViewModel);
    }
}