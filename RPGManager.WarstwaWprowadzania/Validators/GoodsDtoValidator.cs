using Microsoft.EntityFrameworkCore;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaWprowadzania.Dtos;

namespace RPGManager.WarstwaWprowadzania.Validators
{
    // sprawdzić gdzie dokłądnie jest wykorzystywany i zamienic goods na goodsDto - zrobione
    // TODO do walidacji dodać sprawdzenie, czy dane dobro o takiej samej nazwie już nie istnieje.
    public class GoodsDtoValidator : IValidator<GoodsDto>
    {
        private readonly IDataContext _context;

        public GoodsDtoValidator(IDataContext context)
        {
            _context = context;
        }

        public Result<GoodsDto> Validate(GoodsDto goodsDto)
        {
            Result<GoodsDto> goodsDtoValidator = new Result<GoodsDto>
            {
                IsSuccessful = true,
                Message = "ok",
                obj = goodsDto
            };

            if (string.IsNullOrWhiteSpace(goodsDto.Name))
            {
                goodsDtoValidator.IsSuccessful = false;
                goodsDtoValidator.Message = "Brak wprowadzonej nazwy towaru";
            }
            if (goodsDto.Price < 0)
            {
                goodsDtoValidator.IsSuccessful = false;
                goodsDtoValidator.Message = "Nieprawidłowa cena towaru";
            }
            if (goodsDto.Price == 0)
            {
                goodsDtoValidator.IsSuccessful = false;
                goodsDtoValidator.Message = "Nieprawidłowa cena. Cena nie może równać się 0";
            }
            if (goodsDto.Price == null)
            {
                    goodsDtoValidator.IsSuccessful = false;
                    goodsDtoValidator.Message = "Cena nie może być null";
            }

            var existingGoods = _context.Goods.FirstOrDefault(g => g.Name == goodsDto.Name);
            if (existingGoods != null)
            {
                goodsDtoValidator.IsSuccessful = false;
                goodsDtoValidator.Message = "Towar o takiej nazwie już istnieje";
            }


            return goodsDtoValidator;
        }
    }
}
