using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGManager.Dtos;
using RPGManager.Models;

namespace WarstwaWprowadzania.Services.Interfaces
{
    public interface ICountryService
    {
        IEnumerable<Country> GetCountries();
        Country GetCountry(int id);
        ValidatorResult<Country> AddCountry(CountryDto countryDto);
        void UpdateCountry(int id, CountryDto countryDto);
        void DeleteCountry(int id);
    }
}
