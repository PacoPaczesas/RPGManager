﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGManager.Data;
using RPGManager.Dtos;
using RPGManager.Models;
using RPGManager.Services.Interfaces;
using RPGManager.Validators;

namespace WarstwaWprowadzania.Services
{

        public class CountryService : ICountryService
        {
            private readonly DataContext _context;
            private readonly IValidator<Country> _CountryValidator;

        public CountryService(DataContext context, IValidator<Country> CountryValidator)
            {
                _context = context;
                _CountryValidator = CountryValidator;
            }

            public IEnumerable<Country> GetCountries()
            {
                return _context.Countries.OrderBy(c => c.Id).ToList();
            }

            public Country GetCountry(int id)
            {
                return _context.Countries.FirstOrDefault(c => c.Id == id);
            }


        public ValidatorResult<Country> AddCountry(CountryDto countryDto)
        {
            ValidatorResult<Country> CountryValidator = new ValidatorResult<Country>();

            var country = new Country()
            {
                Name = countryDto.Name,
                Capital = countryDto.Capital
            };

            CountryValidator = _CountryValidator.Validate(country);

            if (CountryValidator.IsCompleate)
            {
                _context.Countries.Add(country);
                _context.SaveChanges();

                return (CountryValidator);
            }
            return (CountryValidator);
        }


        public void UpdateCountry(int id, CountryDto countryDto)
        {
            var country = _context.Countries.Find(id);
            if (country == null) return;

            country.Name = countryDto.Name;
            country.Capital = countryDto.Capital;

            _context.Countries.Update(country);
            _context.SaveChanges();
        }

        public void DeleteCountry(int id)
        {
            var country = _context.Countries.Find(id);
            if (country == null) return;

            _context.Countries.Remove(country);
            _context.SaveChanges();
        }

    }

}
