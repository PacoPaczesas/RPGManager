using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using RPGManager.WarstwaWprowadzania.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGManager.WarstwaWprowadzania.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<Users> _userManager;
        private readonly IDataContext _context;
        private readonly IValidator<Users> _userValidator;

        public UsersService(UserManager<Users> userManager, IDataContext context, IValidator<Users> userValidator)
        {
            _userManager = userManager;
            _context = context;
            _userValidator = userValidator;
        }

        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> GetUserByUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<Result<Users>> AddUser(UserDto userDto)
        {
            var user = new Users
            {
                UserName = userDto.Login,
                Email = userDto.Login,
                Role = userDto.Role
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                return new Result<Users> { IsSuccessful = false, Message = string.Join(", ", result.Errors.Select(e => e.Description)) };
            }

            return new Result<Users> { IsSuccessful = true, obj = user };
        }

        public async Task<Result<Users>> UpdateUser(string userName, UserDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return new Result<Users> { IsSuccessful = false, Message = "Użytkownik nie został znaleziony" };
            }

            user.UserName = userDto.Login;
            user.Email = userDto.Login;
            user.Role = userDto.Role;

            var validationResult = _userValidator.Validate(user);
            if (!validationResult.IsSuccessful)
            {
                return validationResult;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new Result<Users> { IsSuccessful = true, obj = user };
        }

        public async Task<Result<Users>> DeleteUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return new Result<Users> { IsSuccessful = false, Message = "Użytkownik nie został znaleziony" };
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new Result<Users> { IsSuccessful = true };
        }
    }
}
