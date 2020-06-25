using NHibernate;
using Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class PersonModel
    {
        public PersonModel() { }
        public PersonModel(Person entity)
        {
            Iin = entity.Iin;
            FullName = $"{entity.LastName} {entity.FirstName} {entity.MiddleName}";
            Birthday = entity.Birthday;
        }
        public string Iin { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
    }
    public class PersonCreateModel
    {
        public PersonCreateModel() { }
        public PersonCreateModel(Person entity)
        {
            Id = entity.Id;
            Iin = entity.Iin;
            LastName = entity.LastName;
            FirstName = entity.FirstName;
            MiddleName = entity.MiddleName;
            Birthday = entity.Birthday;
        }

        public long Id { get; set; }
        public string Iin { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthday { get; set; }

        public List<KeyValuePair<string, string>> Validate(ISession session)
        {
            var errors = new List<KeyValuePair<string, string>>();

            return errors;
        }

        public void Save(ISession session)
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    var employee = new Person
                    {
                        Iin = Iin,
                        LastName = LastName,
                        FirstName = FirstName,
                        MiddleName = MiddleName,
                        Birthday = Birthday
                    };
                    session.Save(employee);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    throw new Exception($"Данные не сохранились! {exception}");
                }
            }
        }
    }
}
