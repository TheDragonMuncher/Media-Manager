using Microsoft.AspNetCore.Mvc;

namespace MediaManager.MVC.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

    }
}
