using Rithmschool.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rithmschool.Core.Interfaces
{
    public interface IAuthenticationRepository: IGenericRepository<User>
    {
        Task<User> Loggin(UserLoginCustom usuario);
        Task<User> ValidateEmail(User usuario);
        Task<User> GetUserByEmail(string user);
    }
}
