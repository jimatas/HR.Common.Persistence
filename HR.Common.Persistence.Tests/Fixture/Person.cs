using HR.Common.Persistence.Entities;

using System.Collections.Generic;

namespace HR.Common.Persistence.Tests.Fixture
{
    public class Person : EntityBase<int>
    {
        public Person() { }
        public Person(int id) => Id = id;

        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public int? Age { get; set; }
        public ICollection<Person> Friends { get; set; }
        public Book FavoriteBook { get; set; }
    }
}
