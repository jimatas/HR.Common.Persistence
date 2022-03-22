using HR.Common.Persistence.Entities;

using System.Collections.Generic;

namespace HR.Common.Persistence.Tests.Integration.Fixture
{
    public class Message : EntityBase<int>
    {
        public Message() { }
        public Message(int id) : base(id) { }

        public string Text { get; set; }

        public Person Sender { get; set; }
        public ICollection<Person> Recipients { get; set; } = new HashSet<Person>();
    }
}
