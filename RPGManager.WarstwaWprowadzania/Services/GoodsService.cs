using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using RPGManager.WarstwaWprowadzania.Validators;


namespace RPGManager.WarstwaWprowadzania.Services;
public class GoodsService : IGoodsService
{
    private readonly IDataContext _context;
    private readonly IValidator<Goods> _GoodsValidator;

    public GoodsService(IDataContext context, IValidator<Goods> GoodsValidator)
    {
        _context = context;
        _GoodsValidator = GoodsValidator;
    }

    public ValidatorResult<Goods> AddNewGoods(GoodsDto goodsDto)
    {
        ValidatorResult<Goods> GoodsValidator = new ValidatorResult<Goods>();

        var goods = new Goods();
        {
            goods.Name = goodsDto.Name;
            goods.Price = goodsDto.Price;
        }

        GoodsValidator = _GoodsValidator.Validate(goods);

        if (GoodsValidator.IsCompleate)
        {
            _context.Goods.Add(goods);
            _context.SaveChanges();
        }
        return GoodsValidator;
    }

    public IEnumerable<Goods> GetGoods()
    {
        var goods = _context.Goods;
        return goods;
    }

    public Goods DeleteGoods(int id)
    {
        var goods = _context.Goods.FirstOrDefault(g => g.Id == id);
        if (goods == null)
        {
            return null;
        }

        _context.Goods.Remove(goods);
        _context.SaveChanges();

        return goods;
    }

    public bool RemoveGoodsFromCountry(int countryId, int goodsId)
    {
        var countryGoods = _context.CountryGoods
            .FirstOrDefault(cg => cg.CountryId == countryId && cg.GoodsId == goodsId);

        if (countryGoods == null)
        {
            return false;
        }

        _context.CountryGoods.Remove(countryGoods);
        _context.SaveChanges();

        return true;
    }


}
