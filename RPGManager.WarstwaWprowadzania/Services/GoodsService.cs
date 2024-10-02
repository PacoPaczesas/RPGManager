using Microsoft.EntityFrameworkCore;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using RPGManager.WarstwaWprowadzania.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGManager.WarstwaWprowadzania.Services
{
    public class GoodsService : IGoodsService
    {
        private readonly IDataContext _context;
        private readonly IValidator<Goods> _goodsValidator;

        public GoodsService(IDataContext context, IValidator<Goods> goodsValidator)
        {
            _context = context;
            _goodsValidator = goodsValidator;
        }

        public async Task<Result<Goods>> AddNewGoodsAsync(GoodsDto goodsDto)
        {
            Result<Goods> goodsValidator = new Result<Goods>();

            var goods = new Goods()
            {
                Name = goodsDto.Name,
                Price = goodsDto.Price
            };

            goodsValidator = _goodsValidator.Validate(goods);

            if (goodsValidator.IsSuccessful)
            {
                await _context.Goods.AddAsync(goods);
                _context.SaveChanges();
            }
            return goodsValidator;
        }

        // to nie jest chyba dobrze zrobione???
        public async Task<IEnumerable<Goods>> GetGoodsAsync()
        {
            return await _context.Goods.ToListAsync();
        }

        public async Task<bool> AssignGoodsToCountryAsync(int countryId, int goodId)
        {
            var country = await _context.Countries.FindAsync(countryId);
            var goods = await _context.Goods.FindAsync(goodId);

            if (country == null || goods == null)
            {
                return false;
            }

            var countryGoods = new CountryGoods { CountryId = countryId, GoodsId = goodId };
            _context.CountryGoods.Add(countryGoods);
            _context.SaveChanges();

            return true;
        }

        public async Task<Goods> DeleteGoodsAsync(int id)
        {
            var goods = await _context.Goods.FindAsync(id);
            if (goods == null)
            {
                return null;
            }

            _context.Goods.Remove(goods);
            _context.SaveChanges();
            return goods;
        }

        public async Task<bool> RemoveGoodsFromCountryAsync(int countryId, int goodsId)
        {
            var countryGoods = await _context.CountryGoods
                .FirstOrDefaultAsync(cg => cg.CountryId == countryId && cg.GoodsId == goodsId);

            if (countryGoods == null)
            {
                return false;
            }

            _context.CountryGoods.Remove(countryGoods);
            _context.SaveChanges();
            return true;
        }
    }
}
