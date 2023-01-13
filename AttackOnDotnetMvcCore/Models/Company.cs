using System.ComponentModel.DataAnnotations;

namespace AttackOnDotnetMvcCore.Models
{
    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Date)] public DateTime RegisterDate { get; set; }
    }
}