using System;
using System.Collections.Generic;

#nullable disable

namespace Rithmschool.Core.DTOs
{
    public partial class UserDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
