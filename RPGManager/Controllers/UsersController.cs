using Microsoft.AspNetCore.Mvc;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            var users = await _usersService.GetUsers();
            if (users == null || !users.Any())
            {
                return NotFound("Lista użytkowników jest pusta");
            }
            return Ok(users);
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<Users>> GetUserByUserName(string userName)
        {
            var user = await _usersService.GetUserByUserName(userName);
            if (user == null)
            {
                return NotFound("Użytkownik o podanym UserName nie istnieje");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Nieprawidłowe dane użytkownika.");
            }

            var result = await _usersService.AddUser(userDto);
            if (!result.IsSuccessful)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetUserByUserName), new { userName = result.obj.UserName }, result.obj);
        }

        [HttpPut("{userName}")]
        public async Task<ActionResult> UpdateUser(string userName, [FromBody] UserDto userDto)
        {
            var result = await _usersService.UpdateUser(userName, userDto);
            if (!result.IsSuccessful)
            {
                return BadRequest(result.Message);
            }
            return Ok("Dane użytkownika zostały zaktualizowane.");
        }

        [HttpDelete("{userName}")]
        public async Task<ActionResult> DeleteUser(string userName)
        {
            var result = await _usersService.DeleteUser(userName);
            if (!result.IsSuccessful)
            {
                return NotFound(result.Message);
            }
            return Ok("Użytkownik został usunięty.");
        }
    }
}
