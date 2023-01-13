using AttackOnDotnetMvcCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttackOnDotnetMVC.Controllers
{
    public class MitreController : Controller
    {
        private static List<Technique> techniques = new List<Technique>()
            {
                new Technique()
                {
                    ID= 1059,
                    Name="Command and Scripting Interpreter",
                    TacticName="Execution",
                    ShortDescription="",
                    LongDescription="",
                },
                new Technique()
                {
                    ID= 1202,
                    Name="Indirect Command Execution",
                    TacticName="Defense Evasion",
                    ShortDescription="",
                    LongDescription="",
                },
            };

        // GET: Mitre
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Techniques()
        {
            return View(techniques);
        }

        public ActionResult Technique(int id)
        {
            var technique = techniques.FirstOrDefault(x => x.ID == id);
            return View(technique);
        }
    }
}