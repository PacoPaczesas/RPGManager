using System.ComponentModel.DataAnnotations;

namespace RPGManager.WarstwaDomenowa.Models
{
    public class NPC
    {
        public NPC(int exp, int strength, int might)
        {
            Exp = exp;
            Strength = strength;
            Might = might;
            AssignLvl();
            AssignHp();
            AssignAC();
            ResetCurrentHP();
        }


        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; } //  foreign key country
        public Country Country { get; set; }
        public ICollection<Note> Notes { get; set; }
        public int Strength { get; set; }
        public int Might { get; set; }
        public int HP { get; set; }
        public int CurrentHP { get; set; }
        public int AC { get; set; }
        public int Exp { get; set; }
        public int Lvl { get; set; }

        public void minusHp(int minus)
        {
            CurrentHP -= minus;

            if (CurrentHP > HP)
            {
                CurrentHP = HP;
            }
            if (CurrentHP < 0)
            {
                CurrentHP = 0;
            }
        }
        public void addExp(int value)
        {
            Exp += value;
            AssignLvl();
        }


        public void AssignLvl()
        {
            if (Exp < 100)
            {
                Lvl = 1;
            }
            else if (Exp < 300)
            {
                Lvl = 2;
            }
            else if (Exp < 900)
            {
                Lvl = 3;
            }
            else
            {
                Lvl = 4;
            }
        }

        public void AssignHp()
        {
            HP = Strength * 5 + Lvl * 3;
        }

        public void AssignAC()
        {
            AC = Strength + Lvl;
        }

        public void ResetCurrentHP()
        {
            CurrentHP = HP;
        }

        public int AttackPower()
        {
            Random random = new Random();
            return Strength + Lvl + random.Next(1, Strength);
        }
    }
}
