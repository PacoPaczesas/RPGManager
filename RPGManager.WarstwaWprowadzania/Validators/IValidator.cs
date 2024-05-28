using RPGManager.Models;

namespace RPGManager.Validators
{
    public interface IValidator <T>
    {
        ValidatorResult<T> Validate(T obj);
    }
}
