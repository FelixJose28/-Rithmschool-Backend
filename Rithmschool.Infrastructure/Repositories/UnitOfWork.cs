using Rithmschool.Core.Interfaces;
using Rithmschool.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rithmschool.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RithmschoolContext _context;
        public ITeacherRepository teacherRepository { get; }
        public IBuyRepository buyRepository { get; }
        public IUserRepository userRepository { get; }
        public UnitOfWork(RithmschoolContext context)
        {
            _context = context;
            teacherRepository = new TeacherRepository(_context);
            buyRepository = new BuyRepository(_context);
            userRepository = new UserRepository(_context);
        }



        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
