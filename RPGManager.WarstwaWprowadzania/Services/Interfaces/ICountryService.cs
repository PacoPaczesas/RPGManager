using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;

namespace RPGManager.WarstwaWprowadzania.Services.Interfaces
{
    public interface ICountryService
    {
        IEnumerable<Country> GetCountries();
        Country GetCountry(int id);
        Result<Country> AddCountry(CountryDto countryDto);
        Result<Country> UpdateCountry(int id, CountryDto countryDto);
        Country DeleteCountry(int id);
    }
}
