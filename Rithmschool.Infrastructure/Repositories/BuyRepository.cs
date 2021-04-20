using Rithmschool.Core.Entities;
using Rithmschool.Core.Interfaces;
using Rithmschool.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rithmschool.Infrastructure.Repositories
{
    public class BuyRepository: GenericRepository<Buy>, IBuyRepository
    {
        private readonly RithmschoolContext _context;
        public BuyRepository(RithmschoolContext context):base(context)
        {
            _context = context;
        }
    }
}
