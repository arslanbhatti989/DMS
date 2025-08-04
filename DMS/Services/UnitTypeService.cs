using DMS.Data;
using DMS.Models;
using DMS.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DMS.Services
{
    public class UnitTypeService : IUnitTypeRepository
    {
        private readonly ApplicationDbContext db;
        public UnitTypeService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<UnitType>> GetAll()
        {
            var list = await db.UnitTypes.Include(a => a.Project).ToListAsync();
            return list;
        }
        public async Task<UnitType> GetDetails(int id)
        {
            var details = await db.UnitTypes.Include(a => a.Project).Where(a => a.Unity_Type_Id == id).FirstOrDefaultAsync() ?? new UnitType();
            return details;
        }
    }
}
