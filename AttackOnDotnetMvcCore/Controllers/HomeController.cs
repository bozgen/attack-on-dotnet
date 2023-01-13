using Microsoft.AspNetCore.Mvc;

namespace AttackOnDotnetMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}