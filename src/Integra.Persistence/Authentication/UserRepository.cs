using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using ContentCaptureApi;
using Integra.Domain.Authentication;
using Integra.Persistence.Settings;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

using User = Integra.Domain.Authentication.User;
using Task = System.Threading.Tasks.Task;

namespace Integra.Persistence.Authentication
{
    public class UserRepository : IUserRepository
    {
        private readonly IEnumerable<User> _authUsers;   
        
        
        public UserRepository(IOptions<AuthenticationSettings> settings)
        {
            _authUsers = new List<User>()
            {
                new User(settings.Value.UserName, settings.Value.UserPassword)
            };
        }


        public Task<User?> Authenticate(string username, string userpassword)
        {
            var user = Task.Run(() =>  _authUsers.FirstOrDefault(
                user => user.UserName == username && user.UserPassword == userpassword));

            return user;
        }

    }
}
