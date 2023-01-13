using System.ComponentModel.DataAnnotations;

namespace AttackOnDotnetMvcCore.Models
{
    public class TestResult
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string TechniqueID { get; set; }
        [DataType(DataType.Date)] public DateTime TestDate { get; set; }
        public bool Result { get; set; }
        public int TestNumber { get; set; }
    }
}