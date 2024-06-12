using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;

namespace RPGManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IDataContext _context;
        private readonly IGoodsService _goodsService;

        public GoodsController(IDataContext context, IGoodsService goodsService)
        {
            _context = context;
            _goodsService = goodsService;
        }

        // adres POST: api/Goods
        [HttpPost]
        public ActionResult<Result<Goods>> PostGoods([FromBody]GoodsDto goodsDto)
        {
            Result<Goods> GoodsValidator = new Result<Goods>();
            GoodsValidator = _goodsService.AddNewGoods(goodsDto);

            if (!GoodsValidator.IsSuccessful)
            {
                return BadRequest(GoodsValidator.Message);
            }
            return Ok(goodsDto);
        }

        //adres GET: api/Goods
        [HttpGet]
        public ActionResult<IEnumerable<Goods>> GetGoods()
        {
            var goods = _goodsService.GetGoods();
            if (goods == null)
            {
                return NotFound("Lista towarów jest pusta");
            }
            return Ok(goods);

        }

        [HttpPost("Przypisz dobro do kraju")]
        public ActionResult AssignGoodsToCountry(int countryId, int goodId)
        {
            var country = _context.Countries.FirstOrDefault(c => c.Id == countryId);
            var goods = _goodsService.GetGoods();

            if (goods == null || country == null)
            {
                return NotFound("Wprowadzono błędne Id");
            }
            var countryGoods = new CountryGoods { CountryId = countryId, GoodsId = goodId };
            _context.CountryGoods.Add(countryGoods);
            _context.SaveChanges();

            return Ok("Dobra przypisane do kraju");

        }

        [HttpDelete("{id}")]
        public ActionResult<Goods> DeleteGoods(int id)
        {
            var goods = _goodsService.DeleteGoods(id);
            if (goods == null)
            {
                return NotFound("Towar o podanym ID nie istnieje");
            }

            return Ok("Towar usunięty pomyślnie");
        }

        [HttpDelete("Usuń wskazane dobro ze wskazanego kraju")]
        public ActionResult RemoveGoodsFromCountry(int countryId, int goodsId)
        {
            bool result = _goodsService.RemoveGoodsFromCountry(countryId, goodsId);
            if (!result)
            {
                return NotFound("Nie znaleziono przypisania dobra do kraju o podanych ID.");
            }

            return Ok("Przypisanie dobra do kraju zostało usunięte.");
        }


    }





}
