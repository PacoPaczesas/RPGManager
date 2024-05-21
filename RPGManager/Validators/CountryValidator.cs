using RPGManager.Models;

namespace RPGManager.Validators
{


    /// <summary>
    /// zwraca validatorResult
    /// </summary>
    public class CountryValidator : IValidator<Country>
    {
        public ValidatorResult<Country> Validate(Country country)
        {
            ValidatorResult<Country> CountryValidator = new ValidatorResult<Country>();
            CountryValidator.IsCompleate = true;
            CountryValidator.Message = "ok";
            CountryValidator.obj = country;

            if (country.Name.Length < 1) 
            {
                CountryValidator.IsCompleate = false;
                CountryValidator.Message = "Brak wprowadzonej nazwy kraju";
            }
            if (country.Capital == null)
            {
                CountryValidator.IsCompleate = false;
                CountryValidator.Message = "Brak wprowadzonej nazwy stolicy kraju";
            }

            return CountryValidator;
        }

    }
}