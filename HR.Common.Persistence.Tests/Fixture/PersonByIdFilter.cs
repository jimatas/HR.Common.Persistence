using System.Linq;

namespace HR.Common.Persistence.Tests.Fixture
{
    public class PersonByIdFilter : IQueryableFilter<Person>
    {
        private readonly int id;
        public PersonByIdFilter(int id) => this.id = id;
        public IQueryable<Person> Filter(IQueryable<Person> sequence) => sequence.Where(p => p.Id == id);
    }
}
