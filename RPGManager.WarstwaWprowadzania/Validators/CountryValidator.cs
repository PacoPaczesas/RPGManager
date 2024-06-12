using RPGManager.WarstwaDomenowa.Models;

namespace RPGManager.WarstwaWprowadzania.Validators
{


    /// <summary>
    /// zwraca validatorResult
    /// </summary>
    public class CountryValidator : IValidator<Country>
    {
        public Result<Country> Validate(Country country)
        {
            Result<Country> CountryValidator = new Result<Country>();
            CountryValidator.IsSuccessful = true;
            CountryValidator.Message = "ok";
            CountryValidator.obj = country;

            if (country.Name.Length < 1)
            {
                CountryValidator.IsSuccessful = false;
                CountryValidator.Message = "Brak wprowadzonej nazwy kraju";
            }
            if (country.Capital == null)
            {
                CountryValidator.IsSuccessful = false;
                CountryValidator.Message = "Brak wprowadzonej nazwy stolicy kraju";
            }

            return CountryValidator;
        }

    }
}