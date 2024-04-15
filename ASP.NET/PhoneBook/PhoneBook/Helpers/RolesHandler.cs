using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PhoneBook.Helpers
{
    internal static class RolesHandler
    {
        internal static bool IsAuthorizedUser(this ISession session)
        {
            return new JwtSecurityToken(session.GetString("token") ?? string.Empty)
                .Claims.Contains(new Claim("rol", "api_access"));
        }

        internal static bool IsAdminUser(this ISession session)
        {
            var t1 = new JwtSecurityToken(session.GetString("token") ?? string.Empty);
            var t2 = t1.Claims.Contains(new Claim("admin_access", "true"));
            return t2;
        }
    }
}
