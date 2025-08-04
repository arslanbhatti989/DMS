using DMS.Models;

namespace DMS.Repositories
{
    public interface ICityRepository
    {
        public Task<List<City>> GetAll();
    }
}
