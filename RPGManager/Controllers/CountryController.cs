using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGManager.Data;
using RPGManager.Dtos;
using RPGManager.Models;
using RPGManager.Services.Interfaces;
using System.Linq;


namespace RPGManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase // Dziedziczy z ControllerBase i dzięki temu mam dostęp do różnicy typów odpowiedzi, np. OK(), NotFound(), BadRequest()
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        // adres GET api/Countries
        [HttpGet]
        public ActionResult<IEnumerable<Country>> GetCountries()
        {
            var countries = _countryService.GetCountries();
            if (countries == null)
            {
                return NotFound(); // Zwraca błąd 404 jeżeli lista Country jest pusta
            }
            return Ok(countries); // zwraca listę krajków
        }


        // adres GET: api/Countries/id
        [HttpGet("{id}")]
        public ActionResult<Country> GetCountry(int id) // używam ActionResult, które daje mi możliwość zwrócenia NotFound
        {
            var country = _countryService.GetCountry(id);

            if (country == null)
            {
                return NotFound();
            }
            return country;
        }

        // adres POST: api/Countries
        [HttpPost]
        public ActionResult<Country> PostCountry([FromBody] CountryDto countryDto) // używam CountryDto do "pobrania" danych. ID uzupełnia się automatycznie gdyż jest to klucz główny z autoinkrementacją
        {
            var country = _countryService.AddCountry(countryDto);
            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country); // pokazuje ścieżkę gdzie dokładnie zostało utworzone Country
        }
        
        // adres PUT: api/Countries/id
        [HttpPut("{id}")]
        public ActionResult UpdateCountry(int id, [FromBody] CountryDto countryDto)
        {
          _countryService.UpdateCountry(id, countryDto);
            return NoContent(); // Zwraca status 204 No Content po pomyślnej aktualizacji
        }

        // adres DELETE: api/Countries/id
        [HttpDelete("{id}")]
        public ActionResult<Country> DeleteCountry(int id)
        {
            _countryService.DeleteCountry(id);
            return NoContent();
        }


    }
}

