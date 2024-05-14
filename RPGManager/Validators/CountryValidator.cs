using RPGManager.Models;

namespace RPGManager.Validators
{


    /// <summary>
    /// zwraca validatorResult
    /// </summary>
    public class CountryValidator : IValidator<Country>
    {
        public Validator Validate(Country country)
        {
            Validator validator = new Validator();
            validator.IsValid = true;
            validator.Message = "ok";

            //validuje nazwe
            if (country.Name.Length < 1) 
            {
                validator.IsValid = false;
                validator.Message = "Brak wprowadzonej nazwy kraju";
            }

            //validuje stolice
            if (country.Capital == null)
            {
                validator.IsValid = false;
                validator.Message = "Brak wprowadzonej nazwy stolicy kraju";
            }

            return validator;
        }

    }
}