using System.Diagnostics;

namespace RPGManager.WarstwaDomenowa.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public List<CountryGoods> CountryGoods { get; set; }

    }
}
