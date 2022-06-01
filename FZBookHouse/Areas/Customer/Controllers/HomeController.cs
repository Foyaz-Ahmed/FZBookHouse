using FZBookHouse.DataAccess.Repository.IRepository;
using FZBookHouse.Models;
using FZBookHouse.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FZBookHouse.Areas.Customer.Controllers
{
   [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger, IUnitofWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return View(productList);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var dataFromDB = _unitOfWork.Product.GetFirstOrDefault(e => e.Id == id, includeProperties: "Category,CoverType");

            ShoppingCart cartObj = new ShoppingCart()
            {
                Product = dataFromDB,
                ProductId = dataFromDB.Id,
            };


            return View(cartObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public IActionResult Details(ShoppingCart cartObject)
        {

            if (ModelState.IsValid)
            {
                //add to the cart
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                cartObject.ApplicationUserId = claims.Value;

                ShoppingCart cartFromDB = _unitOfWork.ShoppingCart.GetFirstOrDefault
                                                                        (e => e.ApplicationUserId == cartObject.ApplicationUserId && e.ProductId == cartObject.ProductId
                                                                        , includeProperties:("Product"));

                if (cartFromDB == null)
                {
                    //no record exist in the database for that product for that user 
                }
                return null;
                    
            }
            else
            {
                var dataFromDB = _unitOfWork.Product.GetFirstOrDefault(e => e.Id == cartObject.ProductId, includeProperties: "Category,CoverType");

                ShoppingCart cartObj = new ShoppingCart()
                {
                    Product = dataFromDB,
                    ProductId = dataFromDB.Id,
                };
                return View(cartObj);
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
