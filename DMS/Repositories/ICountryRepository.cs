using DMS.Models;
using DMS.Models.ViewModels;

namespace DMS.Repositories
{
    public interface ICountryRepository
    {
        public Task<CountryCityListViewModel> GetList();
        public Task<List<Country>> CountryList();
    }
}
