using DMS.Data;
using DMS.Models;
using DMS.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using DMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DMS.Services
{
    public class UsersService : IUsers
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signmanager;
        private readonly IConfiguration _iconfig;
        //private readonly IMailSender _mailSender;
        private readonly IWebHostEnvironment _environment;
        public UsersService(ApplicationDbContext db, UserManager<Users> userManager, SignInManager<Users> signInManager, IConfiguration iconfig, IWebHostEnvironment environment)
        {
            _db = db;
            _userManager = userManager;
            _signmanager = signInManager;
            _iconfig = iconfig;
            _environment = environment;
        }
        public async Task<ResponseClass<Users>> GetAllUsers(ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            var userId = user?.Id;
            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var isAdmin = await _userManager.IsInRoleAsync(user, "SuperAdmin");

            var roleId = _db.UserRoles.Where(ur => ur.UserId == userId).Select(ur => ur.RoleId).FirstOrDefault();
            var rolePermissions = _db.RolePermissions.Where(rp => rp.Role.Name == userRole).ToList();
            var userPermission = rolePermissions.FirstOrDefault(rp => rp.ModelloName.Split(',').Contains("User"));


            var result = new ResponseClass<Users>();

            if (userPermission != null || isAdmin)
            {
                result.HasAccess = true;
                result.Items = await _db.Users.Where(s=>s.Id != userId).ToListAsync();
                result.Other = RolePermissionsHelper.HasPermission(roleId, "User", "All", _db);
                result.CanAdd = RolePermissionsHelper.HasPermission(roleId, "User", "Add", _db);
                result.CanEdit = RolePermissionsHelper.HasPermission(roleId, "User", "Edit", _db);
                result.CanDelete = RolePermissionsHelper.HasPermission(roleId, "User", "Delete", _db);

            }
            else
            {
                result.HasAccess = false;
                result.Items = new List<Users>();
                result.Other = RolePermissionsHelper.HasPermission(roleId, "User", "All", _db);
                result.CanAdd = RolePermissionsHelper.HasPermission(roleId, "User", "Add", _db);
                result.CanEdit = RolePermissionsHelper.HasPermission(roleId, "User", "Edit", _db);
                result.CanDelete = RolePermissionsHelper.HasPermission(roleId, "User", "Delete", _db);
            }

            return result;
        }

        [HttpGet]
        public async Task<ResponseClass<UserViewModel>> GetUserById(ClaimsPrincipal userPrincipal, string? id)
        {
            var result = new ResponseClass<UserViewModel>();
            try
            {
                var currentUser = await _userManager.GetUserAsync(userPrincipal);
                var userId = currentUser?.Id;
                var userRole = (await _userManager.GetRolesAsync(currentUser)).FirstOrDefault();
                var isAdmin = await _userManager.IsInRoleAsync(currentUser, "SuperAdmin");

                //var roleId = _db.UserRoles
                //    .Where(ur => ur.UserId == userId)
                //    .Select(ur => ur.RoleId)
                //    .FirstOrDefault();

                var rolePermissions = _db.RolePermissions
                    .Where(rp => rp.Role.Name == userRole)
                    .ToList();

                var userPermission = rolePermissions
                    .FirstOrDefault(rp => rp.ModelloName.Split(',').Contains("User"));

                if (!string.IsNullOrEmpty(id))
                {
                    var foundUser = await _db.Users.FirstOrDefaultAsync(s => s.Id == id);
                    if ((userPermission != null || isAdmin) && foundUser != null)
                    {

                        var roles = new List<IdentityRole>();
                        var adminRoleId = "";
                        if (isAdmin)
                        {
                            adminRoleId = _db.Roles.Where(s => s.Name == "SuperAdmin").Select(s => s.Id).FirstOrDefault();
                            roles = _db.Roles.Where(s => s.Name != "SuperAdmin").ToList();

                        }
                        else
                        {
                            roles = _db.Roles.Where(s => s.Name != "SuperAdmin").ToList();
                        }
                        result.HasAccess = true;

                        var role = await _userManager.GetRolesAsync(foundUser);
                        var roleId = roles.Where(s => s.Name == role.FirstOrDefault()).FirstOrDefault();
                        var roleName = "";
                        bool isProfile = false;
                        bool isEdit = true;

                        bool isSuperAdmin = false;

                        if (userId == id)
                        {
                            isProfile = true;
                            isEdit = true;
                            roleName = userRole;
                            isSuperAdmin = true;

                        }
                        result.Item = new UserViewModel
                        {

                            IsAdmin = isSuperAdmin,
                            IsEdit = isEdit,
                            IsProfile = isProfile,
                            RoleName = roleName,
                            UserId = foundUser.Id,
                            ParentId = foundUser.ParentId,
                            Name = foundUser.Name,
                            Email = foundUser.Email,
                            PhoneNumber = foundUser.PhoneNumber != null ? foundUser.PhoneNumber : "0000",
                            Password = foundUser.PlainPassword ?? string.Empty,
                            ConfirmPassword = foundUser.PlainPassword ?? string.Empty,
                            File = foundUser.ProfilePicture,
                            RoleId = roleId != null ? roleId.Id : adminRoleId
                        };
                    }
                    else
                    {
                        result.HasAccess = false;
                        result.Item = null;
                    }
                }
            }
            catch (Exception ex)
            {
                result.HasAccess = false;
                result.Item = null;
            }

            return result;
        }
        [HttpGet]

        public async Task<ResponseClass<IdentityRole>> GetRoles()
        {
            var role = new ResponseClass<IdentityRole>();
            try
            {
                role.Items = await _db.Roles.Where(r => r.Name != "SuperAdmin").ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return role;
        }

        [HttpPost]
        public async Task<ResponseClass<UserViewModel>> AddEditUser(UserViewModel model, ClaimsPrincipal currentUser)
        {
            var response = new ResponseClass<UserViewModel>();
            try
            {
                string userId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
                string userRole = currentUser.FindFirstValue(ClaimTypes.Role);
                string fileName = model.File;

                if (model.Picture != null)
                {
                    var extension = Path.GetExtension(model.Picture.FileName).ToLower();
                    string[] permittedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".webp"];
                    if (!permittedExtensions.Contains(extension))
                    {
                        response.Message = "Invalid file type.";
                        return response;
                    }

                    if (model.Picture.Length > 800 * 1024)
                    {
                        response.Message = "File size exceeds 800 KB.";
                        return response;
                    }

                    string folder = Path.Combine(_environment.WebRootPath, "Images");
                    fileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                    string filePath = Path.Combine(folder, fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await model.Picture.CopyToAsync(stream);
                }

                // Update User
                if (!string.IsNullOrEmpty(model.UserId))
                {
                    var user = await _userManager.FindByIdAsync(model.UserId);
                    string token = "";

                    user.Name = model.Name;
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                    user.ProfilePicture = fileName ?? "images1.jpeg";

                    if (!string.IsNullOrEmpty(model.Password) && user.PlainPassword != model.Password)
                    {
                        token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        user.PlainPassword = model.Password;
                    }

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        response.Message = string.Join(", ", result.Errors.Select(e => e.Description));
                        return response;
                    }

                    if (!string.IsNullOrEmpty(token))
                        await _userManager.ResetPasswordAsync(user, token, model.Password);

                    if (!string.IsNullOrEmpty(model.RoleId))
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        await _userManager.RemoveFromRolesAsync(user, roles);
                        var roleName = await _db.Roles.Where(x => x.Id == model.RoleId).Select(x => x.Name).FirstOrDefaultAsync();
                        if (!string.IsNullOrEmpty(roleName))
                            await _userManager.AddToRoleAsync(user, roleName);
                    }

                    response.Item = model;
                    response.CanEdit = true;
                    response.HasAccess = true;
                    response.Message = "User updated successfully.";
                }
                // Create New User
                else
                {
                    var newUser = new Users
                    {
                        Name = model.Name,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        UserName = model.Email,
                        PlainPassword = model.Password,
                        ParentId = userId,
                        ProfilePicture = fileName ?? "images1.jpeg",
                    };

                    var result = await _userManager.CreateAsync(newUser, model.Password);
                    if (!result.Succeeded)
                    {
                        response.Message = string.Join(", ", result.Errors.Select(e => e.Description));
                        return response;
                    }

                    var roleName = await _db.Roles.Where(r => r.Id == model.RoleId).Select(r => r.Name).FirstOrDefaultAsync();
                    await _userManager.AddToRoleAsync(newUser, roleName);

                    response.Item = model;
                    response.CanAdd = true;
                    response.HasAccess = true;
                    response.Message = "User created successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }
        public async Task<ResponseClass<Users>> Delete(string id)
        {
            var response = new ResponseClass<Users>();

            try
            {
                var user = await _userManager.FindByIdAsync(id); // Await was missing

                if (user == null)
                {
                    response.Message = "User not found.";
                    return response;
                }

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    response.Message = "User deleted successfully.";
                    response.Item = user;
                }
                else
                {
                    response.Message = string.Join(", ", result.Errors.Select(e => e.Description));
                }
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

    }

}

