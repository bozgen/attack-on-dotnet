using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AttackOnDotnetMvcCore.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompanyID { get; set; }
        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }
    }
}