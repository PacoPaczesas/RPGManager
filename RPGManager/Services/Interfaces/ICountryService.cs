﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGManager.Dtos;
using RPGManager.Models;

namespace RPGManager.Services.Interfaces
{
    public interface ICountryService
    {
        IEnumerable<Country> GetCountries();
        Country GetCountry(int id);
        Country AddCountry(CountryDto countryDto);
        void UpdateCountry(int id, CountryDto countryDto);
        void DeleteCountry(int id);
    }
}
