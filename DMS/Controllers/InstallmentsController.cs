using DMS.Models.ViewModels;
using DMS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DMS.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class InstallmentsController : Controller
    {
        private readonly IInstallmentsRepository _installmentsRepository;
        public InstallmentsController(IInstallmentsRepository installmentsRepository)
        {
            _installmentsRepository = installmentsRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetInstallmentsCount(int pid)
        {
            var List = new InstallmentListViewModel();
            try
            {
                 List = await _installmentsRepository.GetAll(pid);
                var count =  List?.Installments?.Count();
                
                return Json(new { count = count });
            }
            catch (Exception e)
            {

                //throw;
            }
            return Json(0);
        }
        public async Task<IActionResult> GetPartialList(int pid)
        {
            var list = new InstallmentListViewModel();
            try
            {
                list = await _installmentsRepository.GetAll(pid);
            }
            catch (Exception e)
            {

                //throw;
            }
            return PartialView(list);
        }

    }
}
