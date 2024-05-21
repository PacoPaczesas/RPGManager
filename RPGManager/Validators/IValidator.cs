using Microsoft.AspNetCore.Mvc;
using RPGManager.Models;
using System.ComponentModel.DataAnnotations;

namespace RPGManager.Validators
{
    public interface IValidator <T>
    {
        ValidatorResult<T> Validate(T obj);
    }
}
