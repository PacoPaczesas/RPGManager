using RPGManager.WarstwaDomenowa.Models;

namespace RPGManager.WarstwaWprowadzania.Validators
{
    public interface IValidator<T>
    {
        Result<T> Validate(T obj);
    }
}
