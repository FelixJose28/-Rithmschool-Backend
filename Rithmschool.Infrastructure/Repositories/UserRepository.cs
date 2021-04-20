using Rithmschool.Core.Entities;
using Rithmschool.Core.Interfaces;
using Rithmschool.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rithmschool.Infrastructure.Repositories
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        private readonly RithmschoolContext _context;
        public UserRepository(RithmschoolContext context):base(context)
        {
            _context = context;
        }
    }
}
