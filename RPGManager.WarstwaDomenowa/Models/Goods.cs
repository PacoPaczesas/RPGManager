﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManager.WarstwaDomenowa.Models
{
    public class Goods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<CountryGoods> CountryGoods { get; set; }

    }
}
