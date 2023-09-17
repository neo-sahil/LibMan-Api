using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.EntityDto.UserDto
{
    public class AddUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Usercode { get; set; } = string.Empty;
        public int UserProfileId { get; set; } = 0;
    }
}