using DMS.Data;
using DMS.Models;
using DMS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DMS.Controllers
{
    [Authorize]
    public class CountryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Users> _userManager;
        public CountryController(ApplicationDbContext db, UserManager<Users> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var country = _db.Countries.Where(s => s.Created_By == userId && s.IsDeleted == false).ToList();
                return View(country);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddEditCountry(int? Id)
        {
            try
            {
                CountryViewModel model = new CountryViewModel();
                if (Id != 0 && Id != null)
                {
                    var userId = _userManager.GetUserId(User);
                    var country =await _db.Countries.Where(s => s.Created_By == userId && s.IsDeleted == false).FirstOrDefaultAsync();
                    if (country != null)
                    {
                        model.Country_Id = country.Country_Id;
                        model.Country_Name = country.Country_Name;
                        model.Phone_Code = country.Phone_Code;
                        model.Currency_Code = country.Currency_Code;
                        model.Status_Active = country.Status_Active;
                        model.Country_Code = country.Country_Code;

                    }
                    return PartialView("_AddEditCountry", model);
                }
                else
                {
                    return PartialView("_AddEditCountry", new CountryViewModel());
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEditCountry(CountryViewModel model)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (model.Country_Id != 0)
                {
                    var country = await _db.Countries.Where(s => s.Created_By == userId && s.IsDeleted == false && s.Country_Id == model.Country_Id).FirstOrDefaultAsync();
                    if (country != null)
                    {
                        country.Country_Name = model.Country_Name;
                        country.Country_Code = model.Country_Code;
                        country.Phone_Code = model.Phone_Code;
                        country.Currency_Code = model.Currency_Code;
                        country.Status_Active = model.Status_Active;
                        country.Updated_At = DateTime.Now;
                        country.Updated_By = userId;
                        _db.Countries.Update(country);
                        _db.SaveChanges();
                        return Json(new { success = true });
                    }

                }
                else
                {
                    Country country = new Country
                    {
                        Country_Name = model.Country_Name,
                        Country_Code = model.Country_Code,
                        Phone_Code = model.Phone_Code,
                        Currency_Code = model.Currency_Code,
                        Status_Active = model.Status_Active,
                        Created_At = DateTime.Now,
                        Updated_At = DateTime.Now,
                        Created_By = userId,
                        Updated_By = userId
                    };
                    _db.Countries.Add(country);
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
                var country = await _db.Countries.FindAsync(id);
                if (country != null)
                {
                    country.IsDeleted = true;
                    country.Updated_At = DateTime.Now;
                    country.Updated_By = _userManager.GetUserId(User);
                    _db.Countries.Update(country);
                    await _db.SaveChangesAsync();
                    return Json(new { success = true, message = "Country deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Country not found." });
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
