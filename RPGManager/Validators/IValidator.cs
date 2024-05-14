using Microsoft.AspNetCore.Mvc;
using RPGManager.Models;

namespace RPGManager.Validators
{
    public interface IValidator <T>
    {
        Validator Validate(T obj);
    }
}
