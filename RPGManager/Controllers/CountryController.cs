using Microsoft.AspNetCore.Mvc;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;


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
                return NotFound("Lista Krajów jest pusta");
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
                return NotFound("Kraj o danym Id nie istnieje");
            }
            return Ok(country);
        }


        // adres POST: api/Countries
        [HttpPost]
        public ActionResult<Result<Country>> PostCountry([FromBody] CountryDto countryDto) // używam CountryDto do "pobrania" danych. ID uzupełnia się automatycznie gdyż jest to klucz główny z autoinkrementacją
        {
            Result<Country> countryValidator = new Result<Country>();
            countryValidator = _countryService.AddCountry(countryDto);

            if (!countryValidator.IsSuccessful)
            {
                return BadRequest(countryValidator.Message);
            }

            return CreatedAtAction(nameof(GetCountry), new { id = countryValidator.obj.Id }, countryValidator.obj); // pokazuje ścieżkę gdzie dokładnie zostało utworzone Country
        }



        // adres PUT: api/Countries/id
        [HttpPut("{id}")]
        public ActionResult UpdateCountry(int id, [FromBody] CountryDto countryDto)
        {
            Result<Country> countryValidator = new Result<Country>();
            countryValidator = _countryService.UpdateCountry(id, countryDto);

            if (!countryValidator.IsSuccessful)
            {
                return BadRequest(countryValidator.Message);
            }
            return Ok("Zaktualizowano dane");
        }


        // adres DELETE: api/Countries/id
        [HttpDelete("{id}")]
        public ActionResult<Country> DeleteCountry(int id)
        {
            var country = _countryService.DeleteCountry(id);

            if (country == null)
            {
                return BadRequest("Kraj o danym Id nie istnieje");
            }

            return Ok("Usunięto kraj");
        }

    }
}

