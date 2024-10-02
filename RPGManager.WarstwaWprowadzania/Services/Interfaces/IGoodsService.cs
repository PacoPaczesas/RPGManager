using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPGManager.WarstwaWprowadzania.Services.Interfaces
{
    public interface IGoodsService
    {
        Task<Result<Goods>> AddNewGoodsAsync(GoodsDto goodsDto);
        Task<IEnumerable<Goods>> GetGoodsAsync();
        Task<bool> AssignGoodsToCountryAsync(int countryId, int goodId);
        Task<Goods?> DeleteGoodsAsync(int id);
        Task<bool> RemoveGoodsFromCountryAsync(int countryId, int goodsId);
    }
}
