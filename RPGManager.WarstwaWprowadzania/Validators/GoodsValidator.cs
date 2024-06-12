using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManager.WarstwaWprowadzania.Validators
{
    public class GoodsValidator : IValidator<Goods>
    {
        private readonly IDataContext _context;
        public GoodsValidator(IDataContext context)
        {
            _context = context;
        }

        public ValidatorResult<Goods> Validate(Goods goods)
        {
            ValidatorResult<Goods> GoodsValidator = new ValidatorResult<Goods>();
            GoodsValidator.IsCompleate = true;
            GoodsValidator.Message = "ok";
            GoodsValidator.obj = goods;

            if (goods.Name.Length < 1)
            {
                GoodsValidator.IsCompleate = false;
                GoodsValidator.Message = "Brak wprowadzonej nazwy towaru";
            }
            if (goods.Price < 0 || goods.Name.Length < 1)
            {
                GoodsValidator.IsCompleate = false;
                GoodsValidator.Message = "Nieprawidłowa cena towaru";
            }

            return GoodsValidator;
        }



    }
}
