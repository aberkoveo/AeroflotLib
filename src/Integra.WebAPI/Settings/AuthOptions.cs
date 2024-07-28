
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Integra.WebAPI.Settings
{
    public class AuthOptions
    {
        public const string ISSUER = "IntegraService"; // издатель токена
        public const string AUDIENCE = "IntegraClient"; // потребитель токена
        const string KEY = "myintegrasecret_integrasecretsecretkey!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }

    }


}
