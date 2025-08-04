using DMS.Models;
using DMS.Models.ViewModels;
using DMS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DMS.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]

    public class UnitsController : Controller
    {
        private readonly IUnitRepository _repo;
        private readonly ICountryRepository _countryRepository;
        public UnitsController(IUnitRepository repo, ICountryRepository country)
        {
            _repo = repo;
            _countryRepository = country;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAll());
        }
        public async Task<IActionResult> UnitPdf(int id)
        {
            try
            {
                var m = await _repo.GetPdfDetails(id);
                return View(m);
            }
            catch (Exception e)
            {

                //throw;
            }
            return Json(1);
        }
        public async Task<IActionResult> UnitBooking(int id)
        {
            var model = new UnitViewModel();
            try
            {
                model = await _repo.GetBookDetails(id);
            }
            catch (Exception e)
            {

                //throw;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UnitBooking(UnitViewModel model)
        {
            try
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.UserId = userid;
                var m = await _repo.SaveUpdateUnitBooking(model);
            }
            catch (Exception e)
            {

                //throw;
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteBuyer(int buyerid)
        {
            try
            {
                var success = await _repo.DeleteBuyer(buyerid);
            }
            catch (Exception e)
            {

                throw;
            }
            return Json(1);
        }
        public async Task<IActionResult> _PartialBookingForms(int no)
        {
            var model = new CountryCityListViewModel();
            try
            {
                model = await _countryRepository.GetList();
                ViewBag.no = no;
            }
            catch (Exception e)
            {

                //throw;
            }
            return PartialView(model);
        }
    }
}
