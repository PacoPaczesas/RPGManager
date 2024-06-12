using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;

namespace RPGManager.WarstwaWprowadzania.Services.Interfaces
{
    public interface IGoodsService
    {
        ValidatorResult<Goods> AddNewGoods(GoodsDto goodsDto);
        IEnumerable<Goods> GetGoods();
        Goods DeleteGoods(int id);
        bool RemoveGoodsFromCountry(int countryId, int goodsId);
    }
}
