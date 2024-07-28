using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Integra.WebAPI.Controllers.Authentication.RateLimiting;
using Integra.Persistence.Authentication;
using Integra.Domain.Authentication;
using Integra.WebAPI.Settings;

namespace Integra.WebAPI.Controllers.Authentication
{
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository;
        public AuthController(IUserRepository userRepo) => _userRepository = userRepo;


        [HttpPost]
        [Route("api/login")]
        [LimitRequest(MaxRequests = 2,TimeWindow = 5)]
        public async Task<ActionResult> GetToken([FromBody] User loginUser)
        {
            User? user = await _userRepository.Authenticate(loginUser.UserName, loginUser.UserPassword);

            if (user == null) return Unauthorized();

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, loginUser.UserName) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.ISSUER,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return Ok(token);
        }
    }
}
