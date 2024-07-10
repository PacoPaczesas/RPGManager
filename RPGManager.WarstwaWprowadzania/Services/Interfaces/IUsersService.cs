using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPGManager.WarstwaWprowadzania.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<Users>> GetUsers();
        Task<Users> GetUserByUserName(string userName);
        Task<Result<Users>> AddUser(UserDto userDto);
        Task<Result<Users>> UpdateUser(string userName, UserDto userDto);
        Task<Result<Users>> DeleteUser(string userName);
    }
}
