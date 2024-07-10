using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface ICountryService
{
    Task<IEnumerable<Country>> GetCountries(CancellationToken token);
    Task<Country> GetCountryAsync(int id);
    Task<Result<Country>> AddCountryAsync(CountryDto countryDto);
    Task<Result<Country>> UpdateCountryAsync(int id, CountryDto countryDto);
    Task<Country> DeleteCountryAsync(int id);
}
