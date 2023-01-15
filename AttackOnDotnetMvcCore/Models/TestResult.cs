using System.ComponentModel.DataAnnotations;

namespace AttackOnDotnetMvcCore.Models
{
    public class TestResult
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int TestID { get; set; }
        [DataType(DataType.DateTime)] public DateTime TestDate { get; set; }
        public bool Result { get; set; }
    }
}