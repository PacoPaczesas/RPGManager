using RPGManager.WarstwaDomenowa.Models;

namespace RPGManager.WarstwaWprowadzania.Validators
{
    public interface IValidator<T>
    {
        ValidatorResult<T> Validate(T obj);
    }
}
