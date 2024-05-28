using RPGManager.Dtos;
using RPGManager.Models;

namespace RPGManager.Services.Interfaces
{
    public interface ICountryService
    {
        IEnumerable<Country> GetCountries();
        Country GetCountry(int id);
        ValidatorResult<Country> AddCountry(CountryDto countryDto);
        ValidatorResult<Country> UpdateCountry(int id, CountryDto countryDto);
        Country DeleteCountry(int id);
    }
}
