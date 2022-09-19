using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Services;
using RestSharp;

namespace DataLayer.Repository.Api
{
    public class ApiCountryRepository : ICountryRepository
    {
        private readonly EndpointPrefixDeterminer _prefixDeterminer = EndpointPrefixDeterminer.Instance;
        private const string EndpointSuffix = "/teams/results";
        private List<CountryModel> allCountriesCache = null; // This is just optimization to not bombard the api as much

        public async Task<List<CountryModel>> GetCountries()
        {
            if (allCountriesCache != null)
            {
                return allCountriesCache;
            }
            var client = new RestClient(_prefixDeterminer.GetEndpointPrefix() + EndpointSuffix);
            var response = await client.ExecuteAsync<List<CountryModel>>(new RestRequest());
            if (response.Data != null && response.Data.Any())
            {
                allCountriesCache = response.Data;
                return allCountriesCache;
            }

            System.Diagnostics.Debug.WriteLine(
                "Something went wrong in ApiCountryRepository::GetCountries() during API call... returning empty list of countries");
            return new List<CountryModel>();
        }

        public async Task<CountryModel> GetCountryForFifaCode(string fifaCode)
        {
            if (allCountriesCache != null)
            {
                return allCountriesCache.FirstOrDefault(country => country.FifaCode == fifaCode);
            }
            var client =
                new RestClient(_prefixDeterminer.GetEndpointPrefix() + EndpointSuffix + "?fifa_code=" + fifaCode);
            var response = await client.ExecuteAsync<List<CountryModel>>(new RestRequest());
            if (response.Data != null && response.Data.Any())
            {
                return response.Data.First();
            }
            System.Diagnostics.Debug.WriteLine(
                "Something went wrong in ApiCountryRepository::GetCountries() during API call... returning empty list of countries");
            return new CountryModel();
        }
    }
}