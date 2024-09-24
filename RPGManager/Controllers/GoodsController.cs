using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

//OKOK


namespace RPGManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "GM")]
    public class GoodsController : ControllerBase
    {
        private readonly IGoodsService _goodsService;

        public GoodsController(IGoodsService goodsService)
        {
            _goodsService = goodsService;
        }

        // adres POST: api/Goods
        [HttpPost]
        public async Task<ActionResult<Result<Goods>>> PostGoods([FromBody] GoodsDto goodsDto)
        {
            var GoodsValidator = await _goodsService.AddNewGoodsAsync(goodsDto);

            if (!GoodsValidator.IsSuccessful)
            {
                return BadRequest(GoodsValidator.Message);
            }
            return Ok(GoodsValidator.obj);
        }

        // adres GET: api/Goods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goods>>> GetGoods()
        {
            var goods = await _goodsService.GetGoodsAsync();
            if (goods == null || !goods.Any())
            {
                return NotFound("Lista towarów jest pusta");
            }
            return Ok(goods);
        }

        [HttpPost("AssignGoodsToCountry")]
        public async Task<ActionResult> AssignGoodsToCountry(int countryId, int goodId)
        {
            var result = await _goodsService.AssignGoodsToCountryAsync(countryId, goodId);

            if (!result)
            {
                return NotFound("Wprowadzono błędne Id");
            }

            return Ok("Dobra przypisane do kraju");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGoods(int id)
        {
            var goods = await _goodsService.DeleteGoodsAsync(id);
            if (goods == null)
            {
                return NotFound("Towar o podanym ID nie istnieje");
            }

            return Ok("Towar usunięty pomyślnie");
        }

        [HttpDelete("RemoveGoodsFromCountry")]
        public async Task<ActionResult> RemoveGoodsFromCountry(int countryId, int goodsId)
        {
            var result = await _goodsService.RemoveGoodsFromCountryAsync(countryId, goodsId);
            if (!result)
            {
                return NotFound("Nie znaleziono przypisania dobra do kraju o podanych ID.");
            }

            return Ok("Przypisanie dobra do kraju zostało usunięte.");
        }
    }
}
