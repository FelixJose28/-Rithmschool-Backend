using Rithmschool.Core.Entities;
using Rithmschool.Core.Interfaces;
using Rithmschool.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rithmschool.Infrastructure.Repositories
{
    public class TeacherRepository: GenericRepository<Teacher>, ITeacherRepository
    {
        private readonly RithmschoolContext _context;
        public TeacherRepository(RithmschoolContext context): base(context)
        {
            _context = context;
        }
    }
}
