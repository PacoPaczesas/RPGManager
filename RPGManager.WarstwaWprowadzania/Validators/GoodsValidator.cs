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
            if (goods.Price == 0)
            {
                goodsValidator.IsSuccessful = false;
                goodsValidator.Message = "Nieprawidłowa cena. Cena nie może równać się 0";
            }
            if (goods.Price == null)
            {
                goodsValidator.IsSuccessful = false;
                goodsValidator.Message = "Cena nie może być null";
            }

            return goodsValidator;
        }
    }
}
