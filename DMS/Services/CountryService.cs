using DMS.Data;
using DMS.Models;
using DMS.Models.ViewModels;
using DMS.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DMS.Services
{
    public class CountryService : ICountryRepository
    {
        private readonly ApplicationDbContext db;
        public CountryService(ApplicationDbContext db) 
        {
            this.db = db;
        }
        
        public async Task<CountryCityListViewModel> GetList()
        {
            //var list = await db.Countries.OrderBy(a=>a.Country_Name).ToListAsync();
            //return list;
            var model = new CountryCityListViewModel();
            model.Countries = await db.Countries.OrderBy(a=>a.Country_Name).ToListAsync();
            model.Cities = await db.Cities.OrderBy(a=>a.City_Name).ToListAsync();
            return model;
        }
        public async Task<List<Country>> CountryList()
        {
            var list = await db.Countries.OrderBy(a=>a.Country_Name).ToListAsync();
            return list;
        }
    }
}
