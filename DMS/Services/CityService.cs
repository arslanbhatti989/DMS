using DMS.Data;
using DMS.Models;
using DMS.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DMS.Services
{
    public class CityService : ICityRepository
    {
        private readonly ApplicationDbContext db;
        public CityService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<City>> GetAll()
        {
            var list = await db.Cities.ToListAsync();
            return list;
        }
    }
}
