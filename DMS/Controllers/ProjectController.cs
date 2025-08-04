using DMS.Models;
using DMS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public ProjectController(IProjectRepository repo, ICountryRepository country, ICityRepository cityRepository    )
        {
            _repo = repo;
            _countryRepository = country;
            _cityRepository = cityRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repo.List());
        }
        public async Task<IActionResult> AddUpdate(int id)
        {
            var model = new Project();
            try
            {
                
                if(id != 0)
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
                if(model.Project_Id == 0)
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
    }
}
