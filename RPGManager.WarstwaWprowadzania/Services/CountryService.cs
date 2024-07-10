using Microsoft.EntityFrameworkCore;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using RPGManager.WarstwaWprowadzania.Validators;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class CountryService : ICountryService
{
    private readonly IDataContext _context;
    private readonly IValidator<Country> _countryValidator;

    public CountryService(IDataContext context, IValidator<Country> countryValidator)
    {
        _context = context;
        _countryValidator = countryValidator;
    }

    public async Task<IEnumerable<Country>> GetCountries(CancellationToken token)
    {
        return await _context.Countries
            .Include(c => c.CountryGoods)
            .ThenInclude(cg => cg.Goods)
            .OrderBy(c => c.Id)
            .ToListAsync(token);
    }

    public async Task<Country> GetCountryAsync(int id)
    {
        return await _context.Countries
            .Include(c => c.CountryGoods)
            .ThenInclude(cg => cg.Goods)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Result<Country>> AddCountryAsync(CountryDto countryDto)
    {
        Result<Country> countryValidator = new Result<Country>();

        var country = new Country()
        {
            Name = countryDto.Name,
            Capital = countryDto.Capital
        };

        countryValidator = _countryValidator.Validate(country);

        if (countryValidator.IsSuccessful)
        {
            await _context.Countries.AddAsync(country);
            _context.SaveChanges();
        }
        return countryValidator;
    }

    public async Task<Result<Country>> UpdateCountryAsync(int id, CountryDto countryDto)
    {
        Result<Country> countryValidator = new Result<Country>();
        var country = await _context.Countries.FindAsync(id);

        if (country == null)
        {
            countryValidator.IsSuccessful = false;
            countryValidator.Message = "Nie znaleziono kraju o danym Id";
            return countryValidator;
        }

        country.Name = countryDto.Name;
        country.Capital = countryDto.Capital;

        countryValidator = _countryValidator.Validate(country);

        if (!countryValidator.IsSuccessful)
        {
            return countryValidator;
        }

        _context.Countries.Update(country);
        _context.SaveChanges();

        return countryValidator;
    }

    public async Task<Country> DeleteCountryAsync(int id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country == null) return null;

        _context.Countries.Remove(country);
        _context.SaveChanges();

        return country;
    }
}
