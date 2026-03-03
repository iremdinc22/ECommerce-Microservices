using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class DefaultController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

    }
}
