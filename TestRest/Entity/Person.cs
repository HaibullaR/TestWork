using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Entity
{
    public class Person
    {
        public virtual long Id { get; set; }
        public virtual string Iin { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual DateTime Birthday { get; set; }
    }

    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Id(x => x.Id);
            Map(x => x.Iin);
            Map(x => x.LastName).Not.Nullable();
            Map(x => x.FirstName).Not.Nullable();
            Map(x => x.MiddleName);
            Map(x => x.Birthday);
            Table("Person");
        }
    }
}
