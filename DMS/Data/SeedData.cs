using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DMS.Data;
using DMS.Models;

namespace OrmaProject.Data
{
    public class SeedData
    {
        public static async Task EnsureSeedData(IServiceProvider provider)
        {
            var dbContext = provider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();
            var id = "";
            var mid = "";
            var sid = "";
            if (!await dbContext.Roles.Where(c => c.Name == "Admin").AnyAsync())
            {

                var role = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN".ToUpper() };
                id = role.Id;
                await dbContext.Roles.AddAsync(role);
                await dbContext.SaveChangesAsync();
            }
            if (!await dbContext.Roles.Where(c => c.Name == "SuperAdmin").AnyAsync())
            {

                var role = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "SuperAdmin", NormalizedName = "SUPERADMIN".ToUpper() };
                sid = role.Id;
                await dbContext.Roles.AddAsync(role);
                await dbContext.SaveChangesAsync();
            }
            if (!await dbContext.Roles.Where(c => c.Name == "Buyer").AnyAsync())
            {
                var role = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Buyer", NormalizedName = "Buyer".ToUpper() };
                mid = role.Id;
                await dbContext.Roles.AddAsync(role);
                await dbContext.SaveChangesAsync();
            }
            if (!await dbContext.Roles.Where(c => c.Name == "Employee").AnyAsync())
            {
                var role = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Employee", NormalizedName = "Employee".ToUpper() };
                await dbContext.Roles.AddAsync(role);
                await dbContext.SaveChangesAsync();
            }
            if (!await dbContext.Roles.Where(c => c.Name == "Agent").AnyAsync())
            {
                var role = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Agent", NormalizedName = "Agent".ToUpper() };
                await dbContext.Roles.AddAsync(role);
                await dbContext.SaveChangesAsync();
            }



            if (!await dbContext.Users.Where(c => c.UserName == "admin@admin.com").AnyAsync())
            {
                var hasher = new PasswordHasher<Users>();
                var user = new Users
                {
                    Id = Guid.NewGuid().ToString(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    Email = "admin@admin.com",
                    LockoutEnabled = false,
                    CreatedDate = DateTime.Now,
                    Role = "Admin",
                    PhoneNumber = "1122",
                    EmailConfirmed = true,
                    PlainPassword = "Admin@123",
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    Name = "Admin",
                };
                await dbContext.Users.AddAsync(user);
                var roleid = await dbContext.Roles.Where(a => a.Name == "Admin").FirstOrDefaultAsync();
                if (roleid != null)
                {
                    id = roleid.Id;
                }
                await dbContext.UserRoles.AddAsync(new IdentityUserRole<string> { RoleId = id, UserId = user.Id });

                await dbContext.SaveChangesAsync();
            }

        }
    }
}
