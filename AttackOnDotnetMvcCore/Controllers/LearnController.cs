using AttackOnDotnetMvcCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttackOnDotnetMVC.Controllers
{
    public class LearnController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mitre()
        {
            return View();
        }

        public ActionResult AtomicRedTeam()
        {
            return View();
        }
    }
}