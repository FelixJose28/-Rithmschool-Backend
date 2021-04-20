using System;
using System.Collections.Generic;

#nullable disable

namespace Rithmschool.Core.DTOs
{
    public partial class User
    {
        public User()
        {
            Buys = new HashSet<Buy>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Buy> Buys { get; set; }
    }
}
