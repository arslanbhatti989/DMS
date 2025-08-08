using DMS.Data;
using DMS.Models;
using DMS.Models.ViewModels;
using DMS.Repositories;
using DMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DMS.Controllers
{
    [Authorize]

    public class RolePermisionsController : Controller
    {
        //private readonly IRolePermissions _rolePermission;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signmanager;
        private readonly IConfiguration _iconfig;
        //private readonly IMailSender _mailSender;
        private readonly IWebHostEnvironment _environment;
        public RolePermisionsController(RoleManager<IdentityRole> roleManager, ApplicationDbContext db,
                                        UserManager<Users> userManager, SignInManager<Users> signInManager, IConfiguration iconfig, IWebHostEnvironment environment)
        {

            _roleManager = roleManager;
            _db = db;
            _userManager = userManager;
            _signmanager = signInManager;
            _iconfig = iconfig;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            //return View(await _rolePermission.GetAllRole());
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in User ID

                ViewBag.CanAdd = RolePermissionsHelper.HasPermission(userId, "Users", "Add", _db);
                ViewBag.CanEdit = RolePermissionsHelper.HasPermission(userId, "Users", "Edit", _db);
                ViewBag.CanDelete = RolePermissionsHelper.HasPermission(userId, "Users", "Delete", _db);

                var rolePermissions = await _db.RolePermissions.Include(rp => rp.Role).ToListAsync();

                return View(rolePermissions);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new List<RolePermissions>()); // Return an empty list to avoid null reference errors
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddEditPermisions(int? id)
        {
            try
            {
                var roles = await _db.Roles.Where(s => s.Name != "SuperAdmin").ToListAsync();
                ViewBag.Roles = new SelectList(roles, "Id", "Name"); // Populate dropdown

                var menuNames = SidebarMenuHelper.GetSidebarMenuNames();
                ViewBag.MenuNames = menuNames.Select(name => new SelectListItem
                {
                    Text = name,
                    Value = name
                }).ToList();

                RolePermissionModel model = new RolePermissionModel();

                if (id != null)
                {
                    var permission = await _db.RolePermissions.FindAsync(id);
                    if (permission != null)  // Check if permission exists
                    {
                        model.All = permission.All;
                        model.Edit = permission.Edit;
                        model.AssignedDate = permission.AssignedDate;
                        model.Delete = permission.Delete;
                        model.Add = permission.Add;
                        model.RoleId = permission.RoleId;

                        // ✅ Convert `ModelloName` (comma-separated string) into a list
                        model.SelectedMenuNames = permission.ModelloName?.Split(',').ToList();
                        model.ModuleName = permission.ModelloName;
                    }
                }

                return View(model); // ✅ Return the model correctly
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult AddEditPermisions(RolePermissionModel model)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (model.Id == 0) // Create New
                {
                    var lastId = _db.RolePermissions.OrderByDescending(s => s.Id).Select(s => s.Id).FirstOrDefault();

                    var newRolePermission = new RolePermissions
                    {
                        //Id = lastId + 1,
                        RoleId = model.RoleId,
                        All = model.All,
                        Add = model.Add,
                        Edit = model.Edit,
                        Delete = model.Delete,
                        Readonly = true,
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        CreatedBy = userId,
                        UpdatedBy = userId,
                        ModelloName = model.SelectedMenuNames != null ? string.Join(",", model.SelectedMenuNames) : null // ✅ Ensure null safety
                    };

                    _db.RolePermissions.Add(newRolePermission);
                }
                else // Update Existing
                {
                    var existing = _db.RolePermissions.Find(model.Id);
                    if (existing == null) return NotFound();

                    existing.RoleId = model.RoleId;
                    existing.All = model.All;
                    existing.Add = model.Add;
                    existing.Edit = model.Edit;
                    existing.Delete = model.Delete;
                    //existing.Readonly = model.Readonly;
                    existing.UpdateDate = DateTime.Now;
                    existing.UpdatedBy = userId;

                    existing.ModelloName = model.SelectedMenuNames != null ? string.Join(",", model.SelectedMenuNames) : null; // ✅ Ensure null safety
                }

                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var permission = _db.RolePermissions.Find(id);
            if (permission == null) return NotFound();

            _db.RolePermissions.Remove(permission);
            _db.SaveChanges();
            return Json(new { success = true, message = "Permission deleted successfully." });
        }

        [HttpGet]
        public IActionResult RoleIndex()
        {
            try
            {
                IdentityRole role = new IdentityRole();
                List<IdentityRole> roles = _db.Roles.Where(s => s.Name != "SuperAdmin").ToList();


                if (roles == null) return NotFound();

                return View(roles);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new IdentityRole());
            }
        }
        [HttpGet]
        public async  Task<IActionResult> AddEditRole(string? id)
        {
            try
            {
                IdentityRole role = new IdentityRole();
                if (!string.IsNullOrEmpty(id))
                {
                    role =await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == id);
                    if (role == null) return NotFound();
                    return PartialView("_AddEdit", role);
                }
                return PartialView("_AddEdit", new IdentityRole());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new IdentityRole());
            }
        }

        //  Save Role (Create/Update)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEditRole(IdentityRole model)
        {
            try
            {
                if (model.Name?.Trim().ToLower() == "superadmin" || model.Name?.Trim().ToLower() == "super admin" || model.Name?.Trim().ToLower() == "super admins" || model.Name?.Trim().ToLower() == "superadmins")
                {
                    ModelState.AddModelError("Name", "The role name 'Super Admin' is not allowed. Please choose a different name.");
                    IdentityRole role = new IdentityRole();
                    return PartialView("_AddEdit", model);

                    // ModelState.AddModelError("RoleName", "The role name 'Admin' is not allowed. Please choose a different name.");
                }
                else
                {
                    var existingRole = await _roleManager.FindByIdAsync(model.Id);
                    if (existingRole == null)
                    {
                        await _roleManager.CreateAsync(new IdentityRole(model.Name));

                    }
                    else
                    {
                        existingRole.Name = model.Name;
                        await _roleManager.UpdateAsync(existingRole);
                    }
                    return Json(new { success = true }); // Redirect to Role List
                }


            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    await _roleManager.DeleteAsync(role);
                }
                return Json(new { success = true, message = "Role has been deleted successfully." });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
