using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PhoneBook.Helpers
{
    internal static class RolesHandler
    {
        internal static bool IsAuthorizedUser(this ISession session)
        {
            var encodedToken = GetToken(session);
            if (!string.IsNullOrWhiteSpace(encodedToken))
            {
                return new JwtSecurityToken(encodedToken)
                .Claims.Contains(new Claim("rol", "api_access"), new ClaimsComparer());
            } else
            {
                return false;
            }
        }

        internal static bool IsAdminUser(this ISession session)
        {
            var encodedToken = GetToken(session);
            if (!string.IsNullOrWhiteSpace(encodedToken))
            {
                var token = new JwtSecurityToken(encodedToken);
                return token.Claims.Contains(new Claim("admin_access", "true"), new ClaimsComparer());
            } else
            {
                return false;
            }
        }

        private static string GetToken(ISession session)
        {
            return session.GetString(Constants.TokenName) ?? string.Empty;
        }
    }


    internal class ClaimsComparer : IEqualityComparer<Claim>
    {
        public bool Equals(Claim? x, Claim? y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            return x.Type == y.Type
                && x.Value == y.Value;
        }

        public int GetHashCode([DisallowNull] Claim obj)
        {
            return obj.Type.GetHashCode() + obj.Value.GetHashCode();
        }
    }
}
