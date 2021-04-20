using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rithmschool.Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        ITeacherRepository teacherRepository { get; }
        IBuyRepository buyRepository { get; }
        IUserRepository userRepository { get; }
        Task CommitAsync();

        void Commit();
    }
}
