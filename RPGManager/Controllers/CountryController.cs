using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGManager.Data;
using RPGManager.Dtos;
using RPGManager.Models;
using System.Linq;


namespace RPGManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase // Dziedziczy z ControllerBase i dzięki temu mam dostęp do różnicy typów odpowiedzi, np. OK(), NotFound(), BadRequest()
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }

        // adres GET api/Countries
        [HttpGet]
        /*        public ICollection<Country> GetCountries()
                {
                    return _context.Countries.OrderBy(p => p.Id).ToList();
                }*/
        public ActionResult<IEnumerable<Country>> GetCountries()
        {
            var countries = _context.Countries.OrderBy(p => p.Id).ToList();
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
            var country = _context.Countries.FirstOrDefault(npc => npc.Id == id);

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
            var country = new Country
            {
                Name = countryDto.Name,
                Capital = countryDto.Capital
            };

            _context.Countries.Add(country);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country); // pokazuje ścieżkę gdzie dokładnie zostało utworzone Country
        }

        // adres PUT: api/Countries/id
        [HttpPut("{id}")]
        public ActionResult UpdateCountry(int id, [FromBody] CountryDto countryDto)
        {
            var country = _context.Countries.Find(id);

            if (country == null)
            {
                return NotFound();
            }

            country.Name = countryDto.Name;
            country.Capital = countryDto.Capital;

            _context.Countries.Update(country);
            _context.SaveChanges();

            return NoContent(); // Zwraca status 204 No Content po pomyślnej aktualizacji
        }


        // adres DELETE: api/Countries/id
        [HttpDelete("{id}")]
        public ActionResult<Country> DeleteCountry(int id)
        {
            var country = _context.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            _context.SaveChanges();

            return NoContent();
        }


    }
}

