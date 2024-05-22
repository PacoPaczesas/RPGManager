﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGManager.Data;
using RPGManager.Dtos;
using RPGManager.Models;
using RPGManager.Services.Interfaces;
using RPGManager.Validators;
using System.Diagnostics.Metrics;

namespace RPGManager.Services
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


        public ValidatorResult<Country> UpdateCountry(int id, CountryDto countryDto)
        {
            ValidatorResult<Country> CountryValidator = new ValidatorResult<Country>();
            CountryValidator.obj = _context.Countries.Find(id);

            if (CountryValidator.obj == null)
            {
                CountryValidator.IsCompleate = false;
                CountryValidator.Message = "Nie znaleziono kraju o danym Id";
                return CountryValidator;
            }

            CountryValidator.obj.Name = countryDto.Name;
            CountryValidator.obj.Capital = countryDto.Capital;

            CountryValidator = _CountryValidator.Validate(CountryValidator.obj);

            if (!CountryValidator.IsCompleate)
            {
                return CountryValidator;
            }

            _context.Countries.Update(CountryValidator.obj);
            _context.SaveChanges();

            return CountryValidator;
        }

        public Country DeleteCountry(int id)
        {
            var country = _context.Countries.Find(id);
            if (country == null) return null;

            _context.Countries.Remove(country);
            _context.SaveChanges();

            return country;
        }

    }

}
