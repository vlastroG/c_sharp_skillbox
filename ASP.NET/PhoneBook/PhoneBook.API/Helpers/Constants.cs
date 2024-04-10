namespace PhoneBook.API.Helpers
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol";
                public const string Id = "id";
                public const string Admin = "admin_access";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
                public const string HasAdminAccess = "true";
            }
        }

    }
}
