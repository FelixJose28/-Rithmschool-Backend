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
        IAuthenticationRepository authenticationRepository { get; }
        ICourseRepository courseRepository { get; }
        Task CommitAsync();

        void Commit();
    }
}
