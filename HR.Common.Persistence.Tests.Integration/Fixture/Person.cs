using HR.Common.Persistence.Entities;

using System.Collections.Generic;

namespace HR.Common.Persistence.Tests.Integration.Fixture
{
    public class Person : EntityBase<int>
    {
        public Person() { }
        public Person(int id) : base(id) { }

        public string GivenName { get; set; }
        public string FamilyName { get; set; }

        public ICollection<Message> SentMessages { get; set; } = new HashSet<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new HashSet<Message>();
    }
}
