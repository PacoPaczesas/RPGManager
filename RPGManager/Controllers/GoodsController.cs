using Microsoft.AspNetCore.Mvc;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;

namespace RPGManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IGoodsService _goodsService;

        public GoodsController(IGoodsService goodsService)
        {
            _goodsService = goodsService;
        }

        // adres POST: api/Goods
        [HttpPost]
        public ActionResult<ValidatorResult<Goods>> PostGoods([FromBody]GoodsDto goodsDto)
        {
            ValidatorResult<Goods> GoodsValidator = new ValidatorResult<Goods>();
            GoodsValidator = _goodsService.AddNewGoods(goodsDto);

            if (!GoodsValidator.IsCompleate)
            {
                return BadRequest(GoodsValidator.Message);
            }
            return Ok(goodsDto);
        }



    }





}
