using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Persistence.Authentication
{
    public interface IUserRepository
    {
        Task Authenticate(string username, string password);
    }
}
