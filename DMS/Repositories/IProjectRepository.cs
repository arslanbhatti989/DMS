using DMS.Models;
using DMS.Models.ViewModels;

namespace DMS.Repositories
{
    public interface IProjectRepository
    {
        public Task<List<Project>> List();
        public Task<Project> GetDetails(int projectId);
        public Task<Project> AddUpdate(Project model);
        public Task<bool> Delete(int projectId);
        //public Task<ResponseClass<Project_Seller>> GetProjectSellerList();
    }
}
