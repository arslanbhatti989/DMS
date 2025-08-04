using DMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }   
        public DbSet<Project_Seller> Project_Sellers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UnitType> UnitTypes { get; set; }
        public DbSet<ProjectSellerAdminFee> ProjectSellerAdminFee { get; set; }
        public DbSet<Units> Units { get; set; } 
        public DbSet<Payment_Plans> Payment_Plans { get; set; }
        public DbSet<Installments> Installments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<UnitBuyer> UnitBuyer { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }
    }
}
