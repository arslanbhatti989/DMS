using DMS.Models;
using DMS.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DMS.Repositories
{
    public interface IUsers
    {
        //public Task<List<Users>> GetAllUsers();
        public Task<ResponseClass<Users>> GetAllUsers(ClaimsPrincipal user);
        public Task<ResponseClass<UserViewModel>> GetUserById(ClaimsPrincipal user,string? id);
        public Task<ResponseClass<IdentityRole>> GetRoles();

        public Task<ResponseClass<UserViewModel>> AddEditUser(UserViewModel model,ClaimsPrincipal user);
        public Task<ResponseClass<Users>> Delete(string id);

    }
}
