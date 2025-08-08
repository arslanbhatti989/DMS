using DMS.Data;
using DMS.Models;
using DMS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DMS.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]

    public class UnitTypesController : Controller
    {
        //private readonly IUnitTypeRepository _repo;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Users> _userManager;
        public UnitTypesController(ApplicationDbContext db, UserManager<Users> userManager)
        {
            _db = db;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var unitType = await _db.UnitTypes.Include(s => s.Project).Where(s => s.IsDeleted == false).ToListAsync();
                return View(unitType);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddEditUnitType(int? id)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                UnitType unitType = new UnitType();

                ViewBag.Project =await _db.Projects.Select(s => new SelectListItem { Value = s.Project_Id.ToString(), Text = s.Project_Name }).ToListAsync();
                if (id != 0 && id != null)
                {
                    var getUnitType = await _db.UnitTypes.Include(s => s.Project).Where(s => s.IsDeleted == false && s.Unity_Type_Id == id).FirstOrDefaultAsync();
                    if (unitType != null)
                    {
                        unitType.Unity_Type_Id = getUnitType.Unity_Type_Id;
                        unitType.Unit_Type_Status = getUnitType.Unit_Type_Status;
                        unitType.Unity_Type_Name = getUnitType.Unity_Type_Name;
                        unitType.Project_Id = getUnitType.Project_Id;

                        return PartialView("_AddEditUnitTypes", unitType);
                    }
                    return PartialView("_AddEditUnitTypes", new UnitType());
                }
                else
                {
                    return PartialView("_AddEditUnitTypes", new UnitType());
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEditUnitType(UnitType model)
        {
            try
            {

                var userId = _userManager.GetUserId(User);
                if (model.Unity_Type_Id != 0 )
                {
                    UnitType unitType = new UnitType();
                    var getUnitType = await _db.UnitTypes.Include(s => s.Project).Where(s => s.IsDeleted == false && s.Unity_Type_Id == model.Unity_Type_Id).FirstOrDefaultAsync();
                    if (unitType != null)
                    {
                        getUnitType.Unity_Type_Name = model.Unity_Type_Name;
                        getUnitType.Unit_Type_Status = model.Unit_Type_Status;
                        getUnitType.IsDeleted = false;
                        getUnitType.Updated_By = userId;
                        getUnitType.Updated_At = DateTime.Now;
                        getUnitType.Project_Id = model.Project_Id;
                        _db.Update(getUnitType);
                        _db.SaveChanges();
                        return Json(new { Success = true});
                        
                    }
                    
                }
                else
                {
                    UnitType type = new UnitType
                    {
                        Unity_Type_Name = model.Unity_Type_Name,
                        IsDeleted = false,
                        Unit_Type_Status = model.Unit_Type_Status,
                        Updated_At = DateTime.Now,
                        Created_At = DateTime.Now,
                        Created_By = userId,
                        Project_Id = model.Project_Id,
                        Updated_By = userId

                    };
                    _db.Add(type);
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
                var unitType = await _db.UnitTypes.FindAsync(id);
                if (unitType != null)
                {
                    unitType.IsDeleted = true;
                    unitType.Updated_By = userId;
                    unitType.Updated_At = DateTime.Now;
                    _db.Update(unitType);
                    _db.SaveChanges();
                    return Json(new { Success = true });
                }
                return Json(new { success = true, message = "UnitType deleted successfully." });
            

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return Json(new { success = false, message = "UnitType not found." });


        }
    }
}
