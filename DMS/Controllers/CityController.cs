using DMS.Data;
using DMS.Models.ViewModels;
using DMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace DMS.Controllers
{
    [Authorize]

    public class CityController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Users> _userManager;
        public CityController(ApplicationDbContext db, UserManager<Users> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var city = _db.Cities.Include(s=>s.Country).Where(s => s.Created_By == userId && s.IsDeleted == false).ToList();
                return View(city);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddEditCity(int? Id)
        {
            try
            {
                CityViewModel model = new CityViewModel();
                var userId = _userManager.GetUserId(User);
                ViewBag.Country = await _db.Countries.Where(s => s.Created_By == userId && s.IsDeleted == false).Select(s => new SelectListItem { Value = s.Country_Id.ToString(), Text = s.Country_Name }).ToListAsync();
                if (Id != 0 && Id != null)
                {
                    var city = await _db.Cities.Include(s=>s.Country).Where(s => s.Created_By == userId && s.IsDeleted == false && s.City_Id == Id).FirstOrDefaultAsync();
                    if (city != null)
                    {
                        model.City_Id = city.City_Id;
                        model.Country_Id = (int)city.Country_Id;
                        model.City_Name = city.City_Name != null ? city.City_Name : "Unknow";
                        model.Status_Active = city.Status_Active;

                    }
                    return PartialView("_AddEditCity", model);
                }
                else
                {
                    return PartialView("_AddEditCity", new CityViewModel());
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEditCity(CityViewModel model)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (model.City_Id != 0)
                {
                    var city = await _db.Cities.Where(s => s.Created_By == userId && s.IsDeleted == false && s.City_Id == model.City_Id).FirstOrDefaultAsync();
                    if (city != null)
                    {
                       city.City_Name = model.City_Name;
                       city.Country_Id = model.Country_Id;
                       city.Status_Active = model.Status_Active;
                       city.Updated_At = DateTime.Now;
                       city.Updated_By = userId;
                        _db.Cities.Update(city);
                        _db.SaveChanges();
                        return Json(new { success = true });
                    }

                }
                else
                {
                    City city = new City
                    {
                        City_Name = model.City_Name,
                        Country_Id = model.Country_Id,
                        Status_Active = model.Status_Active,
                        Created_At = DateTime.Now,
                        Updated_At = DateTime.Now,
                        Created_By = userId,
                        Updated_By = userId
                    };
                    _db.Cities.Add(city);
                    _db.SaveChanges();
                    return Json(new { success = true });
                }
                // If not found or some unexpected path
                return Json(new { success = false, message = "Unable to process request." });
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return Json(new { success = false });
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var country = await _db.Cities.FindAsync(id);
                if (country != null)
                {
                    country.IsDeleted = true;
                    country.Updated_At = DateTime.Now;
                    country.Updated_By = _userManager.GetUserId(User);
                    _db.Cities.Update(country);
                    await _db.SaveChangesAsync();
                    return Json(new { success = true, message = "City deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "City not found." });
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return RedirectToAction("Index");

        }
    }
}
