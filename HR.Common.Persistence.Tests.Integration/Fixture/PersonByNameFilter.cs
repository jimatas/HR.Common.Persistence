using System.Linq;

namespace HR.Common.Persistence.Tests.Integration.Fixture
{
    public class PersonByNameFilter : IQueryableFilter<Person>
    {
        private readonly bool usePartialMatching;

        public PersonByNameFilter(bool usePartialMatching = false) => this.usePartialMatching = usePartialMatching;

        public string GivenName { get; set; }
        public string FamilyName { get; set; }

        public IQueryable<Person> Filter(IQueryable<Person> sequence)
        {
            if (!string.IsNullOrEmpty(GivenName))
            {
                sequence = sequence.Where(p => usePartialMatching ? p.GivenName.Contains(GivenName) : p.GivenName.Equals(GivenName));
            }

            if (!string.IsNullOrEmpty(FamilyName))
            {

                sequence = sequence.Where(p => usePartialMatching ? p.FamilyName.Contains(FamilyName) : p.FamilyName.Equals(FamilyName));
            }

            return sequence;
        }
    }
}
