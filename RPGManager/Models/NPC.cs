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

    }
}
