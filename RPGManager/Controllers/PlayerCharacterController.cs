using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RPGManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerCharacterController : ControllerBase
    {
        private readonly IPlayerCharacterService _playerCharacterService;

        public PlayerCharacterController(IPlayerCharacterService playerCharacterService)
        {
            _playerCharacterService = playerCharacterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerCharacter>>> GetPCs(CancellationToken token)
        {
            var pcs = await _playerCharacterService.GetPCs(token);
            if (pcs == null || !pcs.Any())
            {
                return NotFound("Lista postaci graczy jest pusta");
            }
            return Ok(pcs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerCharacter>> GetPC(int id)
        {
            var pc = await _playerCharacterService.GetPC(id);
            if (pc == null)
            {
                return NotFound("Postać o danym Id nie istnieje");
            }
            return Ok(pc);
        }

        [Authorize]
        [HttpGet("mycharacters")]
        public async Task<ActionResult<IEnumerable<PlayerCharacter>>> GetMyCharacters(CancellationToken token)
        {
            var userName = User.Identity.Name; // Pobiera UserName z tokena

            var pcs = await _playerCharacterService.GetPCsByUserName(userName, token);
            if (pcs == null || !pcs.Any())
            {
                return NotFound("Lista postaci graczy jest pusta");
            }
            return Ok(pcs);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PlayerCharacter>> PostPC([FromBody] PlayerCharacterDto playerCharacterDto)
        {
            var userName = User.Identity.Name; // TODO Pobiera UserName z tokena - nie można utworzyć postaci dla kogoś innego poza zalogowanym uzytkownikiem
            var newPlayerCharacterDto = new PlayerCharacterDto
            {
                Name = playerCharacterDto.Name,
                Strength = playerCharacterDto.Strength,
                Might = playerCharacterDto.Might,
                Exp = playerCharacterDto.Exp,
                UserName = userName
            };

            var pc = await _playerCharacterService.AddPC(newPlayerCharacterDto);
            return CreatedAtAction(nameof(GetPC), new { id = pc.Id }, pc);
        }
    }
}
