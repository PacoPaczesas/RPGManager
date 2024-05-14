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
            return validator;
        }

    }
}