using Microsoft.EntityFrameworkCore;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using RPGManager.WarstwaWprowadzania.Validators;


namespace RPGManager.WarstwaWprowadzania.Services
{

    public class CountryService : ICountryService
    {
        // tu było OK
        private readonly IDataContext _context;
        private readonly IValidator<Country> _CountryValidator;

        public CountryService(IDataContext context, IValidator<Country> CountryValidator)
        {
            _context = context;
            _CountryValidator = CountryValidator;
        }





        public IEnumerable<Country> GetCountries()
        {
            //return _context.Countries.OrderBy(c => c.Id).ToList();
            return _context.Countries
                .Include(c => c.CountryGoods)
                .ThenInclude(cg => cg.Goods)
                .OrderBy(c => c.Id).ToList();
        }

        public Country GetCountry(int id)
        {
            //return _context.Countries.FirstOrDefault(c => c.Id == id);
            return _context
                .Countries.Include(c => c.CountryGoods)
                .ThenInclude(cg => cg.Goods)
                .FirstOrDefault(c => c.Id == id);
        }


        public Result<Country> AddCountry(CountryDto countryDto)
        {
            Result<Country> countryValidator = new Result<Country>();

            var country = new Country()
            {
                Name = countryDto.Name,
                Capital = countryDto.Capital
            };

            countryValidator = _CountryValidator.Validate(country);

            if (countryValidator.IsSuccessful)
            {
                _context.Countries.Add(country);
                _context.SaveChanges();
            }
            return countryValidator;
        }


        public Result<Country> UpdateCountry(int id, CountryDto countryDto)
        {
            Result<Country> CountryValidator = new Result<Country>();
            //CountryValidator.obj = _context.Countries.Find(id);
            var Country = _context.Countries.Find(id);

            if (Country == null)
            {
                CountryValidator.IsSuccessful = false;
                CountryValidator.Message = "Nie znaleziono kraju o danym Id";
                return CountryValidator;
            }

            Country.Name = countryDto.Name;
            Country.Capital = countryDto.Capital;

            CountryValidator = _CountryValidator.Validate(Country);

            if (!CountryValidator.IsSuccessful)
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
