using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = Integra.Domain.Authentication.User;
namespace Integra.Persistence.Authentication
{
    public interface IUserRepository
    {
        Task<User?> Authenticate(string username, string password);
    }
}
