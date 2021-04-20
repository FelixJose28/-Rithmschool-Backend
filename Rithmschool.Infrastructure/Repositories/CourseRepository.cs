using Rithmschool.Core.Entities;
using Rithmschool.Core.Interfaces;
using Rithmschool.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rithmschool.Infrastructure.Repositories
{
    public class CourseRepository: GenericRepository<Course>, ICourseRepository
    {
        private readonly RithmschoolContext _context;
        public CourseRepository(RithmschoolContext context):base(context)
        {

        }
    }
}
