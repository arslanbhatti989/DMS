using DMS.Models;
using DMS.Models.ViewModels;
using DMS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DMS.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]

    public class PaymentPlansController : Controller
    {
        private readonly IPaymentPlansRepository _repo;
        private readonly IInstallmentsRepository _repoInstallments;
        public PaymentPlansController(IPaymentPlansRepository repo, IInstallmentsRepository installments)
        {
            _repo = repo;
            _repoInstallments = installments;
        } 
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAll(0));
        }
        public async Task<IActionResult> Installments(int id)
        {
            var list = new InstallmentListViewModel();
            try
            {
                list = await _repoInstallments.GetAll(id);
            }
            catch (Exception e)
            {

                //throw;
            }
            return View(list);
        }
    }
}
