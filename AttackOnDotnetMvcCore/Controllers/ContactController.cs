using Microsoft.AspNetCore.Mvc;

namespace AttackOnDotnetMVC.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
    }
}