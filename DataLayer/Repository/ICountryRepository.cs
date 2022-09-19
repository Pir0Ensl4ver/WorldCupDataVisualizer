using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer
{
    public interface ICountryRepository
    {
        public Task<List<CountryModel>> GetCountries();
        public Task<CountryModel> GetCountryForFifaCode(string fifaCode);
    }
}