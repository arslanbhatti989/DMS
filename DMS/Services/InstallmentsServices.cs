using DMS.Data;
using DMS.Models;
using DMS.Models.ViewModels;
using DMS.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DMS.Services
{
    public class InstallmentsServices : IInstallmentsRepository
    {
        private readonly ApplicationDbContext db;
        public InstallmentsServices(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<InstallmentListViewModel> GetAll(int payid)
        {
            var list = await db.Installments.Include(a => a.Payment_Plans).Where(a=>a.Payment_Plan_Id == payid)
                .Select(x=> new InstallmentListViewModel
                {
                    Payment_Plans = x.Payment_Plans,
                    Installments = db.Installments.OrderBy(a=>a.Sequence_Number).Where(a=>a.Payment_Plan_Id==payid).ToList()
                }).FirstOrDefaultAsync();
            if (list == null)
            {
                list = new InstallmentListViewModel();
            }
            return list;
        }
        public async Task<Installments> GetDetails(int id)
        {
            var details = await db.Installments.Include(a => a.Payment_Plans).Where(a => a.Installment_Id == id).FirstOrDefaultAsync() ?? new Installments();
            return details;
        }
    }
}
