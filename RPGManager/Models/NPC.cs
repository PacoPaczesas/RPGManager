using RPGManager.Dtos;
using System.ComponentModel.DataAnnotations;

namespace RPGManager.Models
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
        public int Strength {  get; set; }
        public int Might {  get; set; }
        public int HP { get; set;}
        public int CurrentHP { get; set; }
        public int AC { get; set;}
        public int Exp { get; set; }
        public int Lvl { get; set; }


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


        // potrzebne jest wcześniejsze utworzenie obiektu klasy walidator, który można gdzieś zapisać po zwróceniu returnem.
        // PYTANIE czy muszę tutaj jeszcze zabezpieczać się przed brakiem takich danych jak MOC i SIŁA, które są wymagane w konstruktorze? Z tego względi, że są w konstruktorze chyba nie.
        public ValidatorResult<NPC> Validate()
        {
            ValidatorResult<NPC> validator = new ValidatorResult<NPC>();
            validator.IsCompleate = true;
            validator.Message = "ok";

            if (Name == null || Name.Length < 2)
            {
                validator.IsCompleate = false;
                validator.Message = "Brak imienia lub imie jest za krótkie (mniej niż co najmniej dwa znaki";
                return validator;
            }
            if (Exp < 0)
            {
                validator.IsCompleate = false;
                validator.Message = "brak wprowadzonej lub błędna wartość exp. Exp nie może być mniejsze niż 0";
                return validator;
            }
            if (Strength < 0)
            {
                validator.IsCompleate = false;
                validator.Message = "brak wprowadzonej lub błędna wartość siła. Siłą nie może być mniejsze niż 0";
                return validator;
            }
            if (Might < 0)
            {
                validator.IsCompleate = false;
                validator.Message = "brak wprowadzonej lub błędna wartość coc. Moc nie może być mniejsze niż 0";
                return validator;
            }

            return validator;
        }

    }
}
