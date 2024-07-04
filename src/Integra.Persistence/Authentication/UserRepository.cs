using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integra.Domain.Authentication;
using Integra.Persistence.Settings;
using Microsoft.Extensions.Options;

namespace Integra.Persistence.Authentication
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthenticationSettings _settings;
        public UserRepository(IOptions<AuthenticationSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<bool> Authenticate(string userName, string userPassword)
        {
            if (await Task.FromResult(
                _settings.UserName == userName && _settings.UserPassword == userPassword))
            {
                return true;
            }

            return false;
        }
    }
}
