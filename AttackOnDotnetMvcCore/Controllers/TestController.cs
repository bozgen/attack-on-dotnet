using AttackOnDotnetMvcCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttackOnDotnetMVC.Controllers
{
    public class TestController : Controller
    {   
        private static List<Test> testList;
        private static List<Technique> techList;

        // GET: Tests

        public ActionResult Index()
        {
            testList = new List<Test> {
                new Test() {
                    ID = 1,
                    Number = 8,
                    Name = "Powershell XML Requests",
                    TechniqueID = 1059,
                    SubTechniqueID = "T1059.001",
                    ShortDescription = "for example, macOS and Linux distributions include some flavor of Unix Shell while Windows installations include the Windows Command Shell and PowerShell.",
                    LongDescription = "for example, macOS and Linux distributions include some flavor of Unix Shell while Windows installations include the Windows Command Shell and PowerShell.for example, macOS and Linux distributions include some flavor of Unix Shell while Windows installations include the Windows Command Shell and PowerShell.",
                    Platform = "Windows",
                    Url = "https://github.com/redcanaryco/atomic-red-team/blob/master/atomics/T1059.001/T1059.001.md#atomic-test-8---powershell-xml-requests",
                    SubTechniqueName = "Powershell"
                },
                new Test() {
                    ID = 2,
                    Number = 9,
                    Name = "Powershell invoke mshta.exe download",
                    TechniqueID = 1059,
                    SubTechniqueID = "T1059.001",
                    ShortDescription = "for example, macOS and Linux distributions include some flavor of Unix Shell while Windows installations include the Windows Command Shell and PowerShell.",
                    LongDescription = "for example, macOS and Linux distributions include some flavor of Unix Shell while Windows installations include the Windows Command Shell and PowerShell.for example, macOS and Linux distributions include some flavor of Unix Shell while Windows installations include the Windows Command Shell and PowerShell.",
                    Platform = "Windows",
                    Url = "https://github.com/redcanaryco/atomic-red-team/blob/master/atomics/T1059.001/T1059.001.md#atomic-test-9---powershell-invoke-mshtaexe-download",
                    SubTechniqueName = "Powershell"
                },
                new Test() {
                    ID = 3,
                    Number = 1,
                    Name = "Indirect Command Execution - pcalua.exe",
                    TechniqueID = 1202,
                    SubTechniqueID = "-",
                    ShortDescription = "for example, macOS and Linux distributions include some flavor of Unix Shell while Windows installations include the Windows Command Shell and PowerShell.",
                    LongDescription = "for example, macOS and Linux distributions include some flavor of Unix Shell while Windows installations include the Windows Command Shell and PowerShell.for example, macOS and Linux distributions include some flavor of Unix Shell while Windows installations include the Windows Command Shell and PowerShell.",
                    Platform = "Windows",
                    Url = "https://github.com/redcanaryco/atomic-red-team/blob/master/atomics/T1059.001/T1059.001.md#atomic-test-9---powershell-invoke-mshtaexe-download",
                    SubTechniqueName = "-"
                },
                new Test()
                {
                    ID = 4,
                    Number = 3,
                    Name = "Indirect Command Execution - conhost.exe",
                    TechniqueID = 1202,
                    SubTechniqueID = "-",
                    ShortDescription = "for example, macOS and Linux distributions include some flavor of Unix Shell while Windows installations include the Windows Command Shell and PowerShell.",
                    LongDescription = "for example, macOS and Linux distributions include some flavor of Unix Shell while Windows installations include the Windows Command Shell and PowerShell.for example, macOS and Linux distributions include some flavor of Unix Shell while Windows installations include the Windows Command Shell and PowerShell.",
                    Platform = "Windows",
                    Url = "https://github.com/redcanaryco/atomic-red-team/blob/master/atomics/T1059.001/T1059.001.md#atomic-test-9---powershell-invoke-mshtaexe-download",
                    SubTechniqueName = "-"
                },
            };
            techList = new List<Technique>
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
            return View(Tuple.Create(testList, techList));
        }

        public ActionResult Details(int id)
        {
            var test = testList.Where(t => t.ID == id).FirstOrDefault();
            var tech = techList.Where(t => t.ID == test.TechniqueID).FirstOrDefault();
            return View(Tuple.Create(test, tech));
        }
    }
}