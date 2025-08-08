using DMS.Data;
using DMS.Models;
using DMS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DMS.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]

    public class ProjectController : Controller
    {
        private readonly IProjectRepository _repo;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private ApplicationDbContext _db;
        private UserManager<Users> _userManager;
        public ProjectController(IProjectRepository repo, ICountryRepository country, ICityRepository cityRepository,ApplicationDbContext db,UserManager<Users> userManager)
        {
            _repo = repo;
            _countryRepository = country;
            _cityRepository = cityRepository;
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _repo.List());

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View();
        }
        public async Task<IActionResult> AddUpdate(int id)
        {
            var model = new Project();
            try
            {

                if (id != 0)
                {
                    model = await _repo.GetDetails(id);
                    ViewData["CountryList"] = new SelectList(await _countryRepository.CountryList(), "Country_Id", "Country_Name", model.Country_Id);
                    ViewData["CityList"] = new SelectList(await _cityRepository.GetAll(), "City_Id", "City_Name", model.City_Id);
                }
                else
                {
                    ViewData["CountryList"] = new SelectList(await _countryRepository.CountryList(), "Country_Id", "Country_Name");
                    ViewData["CityList"] = new SelectList(await _cityRepository.GetAll(), "City_Id", "City_Name");
                }
            }
            catch (Exception e)
            {

                //throw;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddUpdate(Project model)
        {
            try
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (model.Project_Id == 0)
                {
                    model.Created_By = userid;
                }
                model.Updated_By = userid;
                var m = await _repo.AddUpdate(model);
            }
            catch (Exception e)
            {

                //throw;
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var s = await _repo.Delete(id);
            }
            catch (Exception e)
            {

                //throw;
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var model = new Project();
            try
            {
                model = await _repo.GetDetails(id);
            }
            catch (Exception e)
            {

                //throw;
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ProjectSellerIndex()
        {
            try
            {
                Project_Seller model = new Project_Seller();
                var  userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var projectSeller = await _db.Project_Sellers.ToListAsync();
                if (projectSeller != null && projectSeller.Count() != 0)
                {
                    return View(projectSeller);
                }
                else
                {
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ProjectSellerDetails(int? id)
        {
            try
            {
                Project_Seller model = new Project_Seller();
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var projectSeller = await _db.Project_Sellers.Where(s =>  s.Project_Seller_Id == id).ToListAsync();
                if (projectSeller != null && projectSeller.Count() != 0)
                {
                    model = projectSeller.FirstOrDefault();
                    return PartialView("_ProjectSellerDetails", model);
                }
                else
                {
                    return PartialView("_ProjectSellerDetails", new Project_Seller());
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View();
        }
    }
}
