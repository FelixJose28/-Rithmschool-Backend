using Microsoft.EntityFrameworkCore;
using Rithmschool.Core.Entities;
using Rithmschool.Core.Interfaces;
using Rithmschool.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rithmschool.Infrastructure.Repositories
{
    public class AuthenticationRepository: GenericRepository<User>, IAuthenticationRepository
    {
        private readonly RithmschoolContext _context;
        public AuthenticationRepository(RithmschoolContext context):base(context)
        {
            _context = context;
        }
        public async Task<User> Loggin(UserLoginCustom user)
        {
            User userLogger = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
            return userLogger;
        }

        public async Task<User> ValidateEmail(User user)
        {
            User userR = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            return userR;
        }
    }
}
