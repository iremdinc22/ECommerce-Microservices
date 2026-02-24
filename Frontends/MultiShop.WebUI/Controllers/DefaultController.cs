using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class DefaultController : Controller
    {

        public ActionResult Index()
        {
            var user = User.Claims;
            int x ;
            return View();
        }

    }
}
