using DMS.Models;

namespace DMS.Repositories
{
    public interface IUnitTypeRepository
    {
        public Task<List<UnitType>> GetAll();
        public Task<UnitType> GetDetails(int id);
    }
}
