using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace RPGManager.WarstwaDomenowa.Models
{
    public class Users : IdentityUser
    {
        public string Role { get; set; }
        public ICollection<PlayerCharacter> PlayerCharacters { get; set; }
    }
}
