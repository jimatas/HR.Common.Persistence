namespace HR.Common.Persistence.Tests.Fixture
{
    public static class PersonExtensions
    {
        public static string FullName(this Person p) => $"{p.GivenName} {p.FamilyName}";
    }
}
