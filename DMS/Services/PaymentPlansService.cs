using DMS.Data;
using DMS.Models;
using DMS.Models.ViewModels;
using DMS.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DMS.Services
{
    public class PaymentPlansService : IPaymentPlansRepository
    {
        private readonly ApplicationDbContext db;
        public PaymentPlansService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<PaymentPlansViewModel>> GetAll(int proid)
        {
            var list = await db.Payment_Plans.Include(a => a.Project).Select(x=> new PaymentPlansViewModel
            {
                Project_Id= x.Project_Id,
                Payment_Plan_Id= x.Payment_Plan_Id,
                Created_At= x.Created_At,
                Created_By= x.Created_By,
                Plan_Status= x.Plan_Status,
                Plan_Name= x.Plan_Name,
                Project = x.Project,
                Update_At= x.Update_At,
                TotalInstallments = db.Installments.Where(a=>a.Payment_Plan_Id == x.Payment_Plan_Id).Count(),
                Updated_By= x.Updated_By,
                
            }).ToListAsync();
            if (proid != 0)
            {
                list = list.Where(a=>a.Project_Id == proid).ToList();
            }
            return list;
        }
        public async Task<PaymentPlansViewModel> GetDetails(int id)
        {
            var details = await db.Payment_Plans.Include(a => a.Project).Select(x => new PaymentPlansViewModel
            {
                Project_Id = x.Project_Id,
                Payment_Plan_Id = x.Payment_Plan_Id,
                Created_At = x.Created_At,
                Created_By = x.Created_By,
                Plan_Status = x.Plan_Status,
                Plan_Name = x.Plan_Name,
                Project = x.Project,
                Update_At = x.Update_At,
                TotalInstallments = db.Installments.Where(a => a.Payment_Plan_Id == x.Payment_Plan_Id).ToList().Count(),
                Updated_By = x.Updated_By,

            }).FirstOrDefaultAsync() ?? new PaymentPlansViewModel();
            return details;
        }
    }
}
