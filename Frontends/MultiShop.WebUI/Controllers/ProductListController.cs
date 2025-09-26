using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ProductDetail()
        {
            return View();
        }


    }
}
