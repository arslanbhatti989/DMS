using DMS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DMS.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]

    public class UnitTypesController : Controller
    {
        private readonly IUnitTypeRepository _repo;
        public UnitTypesController(IUnitTypeRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAll());
        } 
    }
}
