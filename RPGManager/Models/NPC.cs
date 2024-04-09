namespace RPGManager.Models
{
    public class NPC
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; } //  foreign key country
        public Country Country { get; set; }
        public ICollection<Note> Notes { get; set; }
        public int Strength {  get; set; }
        public int Might {  get; set; }
        public int HP { get; set;}
        public int AC { get; set;}
        public int Exp { get; set; }
        public int Lvl { get; set; }

    }
}
