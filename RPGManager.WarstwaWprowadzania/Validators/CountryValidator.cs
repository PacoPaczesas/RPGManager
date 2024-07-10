using RPGManager.WarstwaDomenowa.Models;

namespace RPGManager.WarstwaWprowadzania.Validators
{
    public class CountryValidator : IValidator<Country>
    {
        public Result<Country> Validate(Country country)
        {
            Result<Country> countryValidator = new Result<Country>();
            countryValidator.IsSuccessful = true;
            countryValidator.Message = "ok";
            countryValidator.obj = country;

            if (string.IsNullOrWhiteSpace(country.Name))
            {
                countryValidator.IsSuccessful = false;
                countryValidator.Message = "Brak wprowadzonej nazwy kraju";
            }
            if (string.IsNullOrWhiteSpace(country.Capital))
            {
                countryValidator.IsSuccessful = false;
                countryValidator.Message = "Brak wprowadzonej nazwy stolicy kraju";
            }

            return countryValidator;
        }
    }
}
