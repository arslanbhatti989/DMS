using DMS.Models;
using DMS.Models.ViewModels;

namespace DMS.Repositories
{
    public interface IPaymentPlansRepository
    {
        public Task<List<PaymentPlansViewModel>> GetAll(int id);
        public Task<PaymentPlansViewModel> GetDetails(int id);
    }
}
