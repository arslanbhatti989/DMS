using DMS.Models;

namespace DMS.Repositories
{
    public interface IProjectRepository
    {
        public Task<List<Project>> List();
        public Task<Project> GetDetails(int projectId);
        public Task<Project> AddUpdate(Project model);
        public Task<bool> Delete(int projectId);
    }
}
