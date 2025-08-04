using DMS.Models;
using DMS.Models.ViewModels;

namespace DMS.Repositories
{
    public interface IInstallmentsRepository
    {
        public Task<InstallmentListViewModel> GetAll(int id);
        public Task<Installments> GetDetails(int id);
    }
}
