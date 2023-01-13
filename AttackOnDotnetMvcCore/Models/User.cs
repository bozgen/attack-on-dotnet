using System.ComponentModel.DataAnnotations;

namespace AttackOnDotnetMvcCore.Models
{
    public class User
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }
    }
}