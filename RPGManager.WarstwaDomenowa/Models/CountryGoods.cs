using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManager.WarstwaDomenowa.Models
{
    public class CountryGoods
    {
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public int GoodsId { get; set; }
        public Goods Goods { get; set; }
    }
}
