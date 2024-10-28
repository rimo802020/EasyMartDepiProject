using EasyMart.Models;
using EasyMart.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyMart.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ViewResult Index()
        {
            

            return View();
        }
    }
}
