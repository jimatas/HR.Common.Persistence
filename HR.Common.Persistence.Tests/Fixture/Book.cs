using HR.Common.Persistence.Entities;

using System.Collections.Generic;

namespace HR.Common.Persistence.Tests.Fixture
{
    public class Book : EntityBase<int>
    {
        public Book() { }
        public Book(int id) => Id = id;

        public string Title { get; set; }
        public ICollection<Person> Authors { get; set; }
        public Genre Genre { get; set; }
    }
}
