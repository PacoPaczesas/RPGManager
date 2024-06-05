using System.Diagnostics;

namespace RPGManager.WarstwaDomenowa.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public ICollection<Goods> Import { get; set; }
    }
}
