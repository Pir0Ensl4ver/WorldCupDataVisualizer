using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Services;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DataLayer.File
{
    public class FileCountryRepository : ICountryRepository
    {
        private readonly EndpointPrefixDeterminer _prefixDeterminer = EndpointPrefixDeterminer.Instance;
        private string FileSuffix = "results.json";

        public async Task<List<CountryModel>> GetCountries()
        {
                using FileStream stream = System.IO.File.OpenRead(_prefixDeterminer.GetFilePrefix() + FileSuffix);
                List<CountryModel> items = await JsonSerializer.DeserializeAsync<List<CountryModel>>(stream);
                if (items == null || items.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("Something went wrong while parsing file..");
                    return new List<CountryModel>();
                }
                return items;
        }

        public async Task<CountryModel> GetCountryForFifaCode(string fifaCode)
        {
            var allCountries = await GetCountries();
            return allCountries.FirstOrDefault(c => c.FifaCode == fifaCode);
        }
    }
}