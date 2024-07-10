using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;

namespace RPGManager.WarstwaWprowadzania.Validators
{
    public class GoodsValidator : IValidator<Goods>
    {
        public Result<Goods> Validate(Goods goods)
        {
            Result<Goods> goodsValidator = new Result<Goods>
            {
                IsSuccessful = true,
                Message = "ok",
                obj = goods
            };

            if (string.IsNullOrWhiteSpace(goods.Name))
            {
                goodsValidator.IsSuccessful = false;
                goodsValidator.Message = "Brak wprowadzonej nazwy towaru";
            }
            if (goods.Price < 0)
            {
                goodsValidator.IsSuccessful = false;
                goodsValidator.Message = "Nieprawidłowa cena towaru";
            }

            return goodsValidator;
        }
    }
}
