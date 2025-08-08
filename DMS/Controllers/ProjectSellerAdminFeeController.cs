using DMS.Data;
using DMS.Models;
using DMS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DMS.Controllers
{
    [Authorize]
    public class ProjectSellerAdminFeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Users> _userManager;
        public ProjectSellerAdminFeeController(ApplicationDbContext db, UserManager<Users> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var projectSellerAdminFees = await _db.ProjectSellerAdminFee.Include(s => s.Project).Where(s => s.IsDeleted == false).ToListAsync();
                return View(projectSellerAdminFees);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddEditProjectSellerAdminFee(int? id)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                ProjectSellerAdminFeeViewModel model = new ProjectSellerAdminFeeViewModel();

                ViewBag.Project = await _db.Projects.Select(s => new SelectListItem { Value = s.Project_Id.ToString(), Text = s.Project_Name }).ToListAsync();
                if (id != 0 && id != null)
                {
                    var projectSellerAdminFee = await _db.ProjectSellerAdminFee.Include(s => s.Project).Where(s => s.IsDeleted == false && s.Project_Seller_Admin_Fee_Id == id).FirstOrDefaultAsync();
                    if (projectSellerAdminFee != null)
                    {
                        model.Admin_Fee_Value = projectSellerAdminFee.Admin_Fee_Value;
                        model.OQoob_Fee_Value = projectSellerAdminFee.OQoob_Fee_Value;
                        model.Admin_Fee_Description = projectSellerAdminFee.Admin_Fee_Description;
                        model.Project_Id = (int)projectSellerAdminFee.Project_Id;
                        model.Project_Seller_Admin_Fee_Id = projectSellerAdminFee.Project_Seller_Admin_Fee_Id;
                        model.IsDeleted = false;
                        model.Created_By = userId;
                        model.Created_At = DateTime.Now;
                        model.Updated_By = userId;
                        model.Updated_At = DateTime.Now;
                        model.Other_Charges = projectSellerAdminFee.Other_Charges;
                        model.OQood_Fee_Description = projectSellerAdminFee.OQood_Fee_Description;
                        model.Rera_Fee = projectSellerAdminFee.Rera_Fee;
                        model.Rera_Fee_Description = projectSellerAdminFee.Rera_Fee_Description;

                        return PartialView("_AddEditAdminFee", model);

                    }
                    return PartialView("_AddEditAdminFee", new ProjectSellerAdminFeeViewModel());
                }
                else
                {
                    return PartialView("_AddEditAdminFee", new ProjectSellerAdminFeeViewModel());
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEditProjectSellerAdminFee(ProjectSellerAdminFeeViewModel model)
        {
            try
            {

                var userId = _userManager.GetUserId(User);
                if (model.Project_Seller_Admin_Fee_Id != 0)
                {
                    var projectSeller = await _db.ProjectSellerAdminFee.Include(s => s.Project).Where(s => s.IsDeleted == false && s.Project_Seller_Admin_Fee_Id == model.Project_Seller_Admin_Fee_Id).FirstOrDefaultAsync();
                    if (projectSeller != null)
                    {
                        projectSeller.Admin_Fee_Value = model.Admin_Fee_Value;
                        projectSeller.OQoob_Fee_Value = model.OQoob_Fee_Value;
                        projectSeller.Admin_Fee_Description = model.Admin_Fee_Description;
                        projectSeller.Other_Charges = model.Other_Charges;
                        projectSeller.OQood_Fee_Description = model.OQood_Fee_Description;
                        projectSeller.Rera_Fee = model.Rera_Fee;
                        projectSeller.Rera_Fee_Description = model.Rera_Fee_Description;
                        projectSeller.Updated_At = DateTime.Now;

                        projectSeller.IsDeleted = false;
                        projectSeller.Updated_By = userId;
                        projectSeller.Updated_At = DateTime.Now;
                        projectSeller.Project_Id = model.Project_Id;

                        _db.Update(projectSeller);
                        _db.SaveChanges();
                        return Json(new { Success = true });

                    }

                }
                else
                {
                    ProjectSellerAdminFee item = new ProjectSellerAdminFee
                    {
                        Admin_Fee_Description = model.Admin_Fee_Description,
                        Admin_Fee_Value = model.Admin_Fee_Value,
                        OQoob_Fee_Value = model.OQoob_Fee_Value,
                        Other_Charges = model.Other_Charges,
                        OQood_Fee_Description = model.OQood_Fee_Description,
                        Rera_Fee = model.Rera_Fee,
                        IsDeleted = false,
                        Updated_At = DateTime.Now,
                        Created_At = DateTime.Now,
                        Created_By = userId,
                        Project_Id = model.Project_Id,
                        Updated_By = userId

                    };
                    _db.Add(item);
                    _db.SaveChanges();
                    return Json(new { Success = true });
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return Json(new { Success = false });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var unitType = await _db.ProjectSellerAdminFee.FindAsync(id);
                if (unitType != null)
                {
                    unitType.IsDeleted = true;
                    unitType.Updated_By = userId;
                    unitType.Updated_At = DateTime.Now;
                    _db.Update(unitType);
                    _db.SaveChanges();
                    return Json(new { Success = true });
                }
                return Json(new { success = true, message = "Project Seller Admin Fee Record deleted successfully." });


            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return Json(new { success = false, message = "Project Seller Admin Fee Record not found." });


        }
    }
}
