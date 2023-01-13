using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AttackOnDotnetMvcCore.Models;

namespace AttackOnDotnetMvcCore.Data
{
    public class AttackOnDotnetMvcCoreContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public AttackOnDotnetMvcCoreContext (DbContextOptions<AttackOnDotnetMvcCoreContext> options)
            : base(options)
        {
        }

        public DbSet<AttackOnDotnetMvcCore.Models.Company> Company { get; set; } = default!;

        public DbSet<AttackOnDotnetMvcCore.Models.Technique> Technique { get; set; }

        public DbSet<AttackOnDotnetMvcCore.Models.Test> Test { get; set; }

        public DbSet<AttackOnDotnetMvcCore.Models.TestResult> TestResult { get; set; }

        public DbSet<AttackOnDotnetMvcCore.Models.User> User { get; set; }

        public DbSet<AttackOnDotnetMvcCore.Models.Platform> Platform { get; set; }
    }
}
