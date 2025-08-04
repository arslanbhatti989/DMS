using DMS.Data;
using DMS.Models;
using DMS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DMS.Services
{

    public class RolePermissionsService : IRolePermissions
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Users> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RolePermissionsService( ApplicationDbContext db, UserManager<Users> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<List<RolePermissions>> GetAllRole()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
           var rolePermission = _db.RolePermissions.Where(s => s.IsDeleted == false && s.CreatedBy == userId).ToListAsync();
            return rolePermission;
        }
    }
}
