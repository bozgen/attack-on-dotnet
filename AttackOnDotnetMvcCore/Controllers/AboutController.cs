using Microsoft.AspNetCore.Mvc;

namespace AttackOnDotnetMVC.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            return View();
        }
    }
}