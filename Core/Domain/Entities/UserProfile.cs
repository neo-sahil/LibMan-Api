using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Common;

namespace Domain.Entities
{
    public class UserProfile : BaseEntity
    {
        public string Name { get; set; }
    }
}