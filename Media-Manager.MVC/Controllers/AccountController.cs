using Microsoft.AspNetCore.Mvc;

namespace Media_Manager.MVC.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

    }
}
