using Microsoft.AspNetCore.Mvc;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


// OKOK

namespace RPGManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        // adres GET api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries(CancellationToken token)
        {
            var countries = await _countryService.GetCountries(token);
            if (countries == null || !countries.Any())
            {
                return NotFound("Lista Krajów jest pusta");
            }
            return Ok(countries);
        }

        // adres GET: api/Countries/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _countryService.GetCountryAsync(id);

            if (country == null)
            {
                return NotFound("Kraj o danym Id nie istnieje");
            }
            return Ok(country);
        }

        // adres POST: api/Countries
        [HttpPost]
        public async Task<ActionResult<Result<Country>>> PostCountry([FromBody] CountryDto countryDto)
        {
            Result<Country> countryValidator = await _countryService.AddCountryAsync(countryDto);

            if (!countryValidator.IsSuccessful)
            {
                return BadRequest(countryValidator.Message);
            }

            return CreatedAtAction(nameof(GetCountry), new { id = countryValidator.obj.Id }, countryValidator.obj);
        }

        // adres PUT: api/Countries/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCountry(int id, [FromBody] CountryDto countryDto)
        {
            Result<Country> countryValidator = await _countryService.UpdateCountryAsync(id, countryDto);

            if (!countryValidator.IsSuccessful)
            {
                return BadRequest(countryValidator.Message);
            }
            return Ok("Zaktualizowano dane");
        }

        // adres DELETE: api/Countries/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> DeleteCountry(int id)
        {
            var country = await _countryService.DeleteCountryAsync(id);

            if (country == null)
            {
                return BadRequest("Kraj o danym Id nie istnieje");
            }

            return Ok("Usunięto kraj");
        }
    }
}
