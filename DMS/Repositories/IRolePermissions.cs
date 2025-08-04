using DMS.Models.ViewModels;
using DMS.Models;

namespace DMS.Repositories
{
    public interface IRolePermissions
    {
        public Task<List<RolePermissions>> GetAllRole();
        //public Task<List<Units>> GetAll();


        //public Task<Units> GetDetails(int id);
        //public Task<UnitViewModel> GetPdfDetails(int id);
        //public Task<UnitViewModel> GetBookDetails(int id);

        //public Task<UnitViewModel> SaveUpdateUnitBooking(UnitViewModel model);
        //public Task<bool> DeleteBuyer(int buyerid);
    }
}
