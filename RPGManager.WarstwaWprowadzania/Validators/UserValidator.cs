using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;

namespace RPGManager.WarstwaWprowadzania.Validators
{
    public class UserValidator : IValidator<Users>
    {
        private readonly IDataContext _context;
        public UserValidator(IDataContext context)
        {
            _context = context;
        }

        public Result<Users> Validate(Users user)
        {
            Result<Users> validator = new Result<Users>();
            validator.IsSuccessful = true;
            validator.Message = "ok";
            validator.obj = user;

            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                validator.IsSuccessful = false;
                validator.Message = "Brak nazwy użytkownika";
                return validator;
            }

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                validator.IsSuccessful = false;
                validator.Message = "Brak hasła";
                return validator;
            }

            if (_context.Users.Any(u => u.UserName == user.UserName))
            {
                validator.IsSuccessful = false;
                validator.Message = "Użytkownik o podanej nazwie już istnieje";
                return validator;
            }

            return validator;
        }
    }
}
