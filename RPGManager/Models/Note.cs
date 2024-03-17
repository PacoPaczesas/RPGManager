﻿namespace RPGManager.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int NPCId { get; set; } // Klucz obcy do NPV
        public NPC NPC { get; set; }
    }
}
