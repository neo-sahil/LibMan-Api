using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index(nameof(User.Usercode),IsUnique = true)]
    [Index(nameof(User.Username),IsUnique = true)]
    public class User : BaseEntity
    {
        public int Id { get; set; } 
        public string Username { get; set; }
        public string Usercode {get; set;}
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string photo {get; set;}
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}