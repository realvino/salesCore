namespace tibs.stem.Authorization.Roles
{
    public static class StaticRoleNames
    {
        public static class Host
        {
            public const string Admin = "Admin";
        }

        public static class Tenants
        {
            public const string Admin = "Admin";

            public const string User = "User";

            public const string Exec = "Sales Executive";
            public const string SaMan = "Sales Manager";
            public const string AssMan = "Assistant Manager";
            public const string GeMan = "General Manager";
        }
    }
}