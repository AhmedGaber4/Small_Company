using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MyDbcontexts : IdentityDbContext<ApplicationUser, ApplicationRole,string>
    {
        public MyDbcontexts(DbContextOptions<MyDbcontexts> options) : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Emloyees { get; set; }

    }
}
