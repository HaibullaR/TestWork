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
            Iin = entity.Iin;
            LastName = entity.LastName;
            FirstName = entity.FirstName;
            MiddleName = entity.MiddleName;
            Birthday = entity.Birthday;
        }

        public string Iin { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthday { get; set; }

        public List<KeyValuePair<string, string>> Validate(ISession session)
        {
            var errors = new List<KeyValuePair<string, string>>();
            var person = session.Query<Person>().FirstOrDefault(x=>x.Iin == Iin);

            if (person != null)
                errors.Add(new KeyValuePair<string, string>("Person", "Данный пользователь уже существует.{дубликат}"));

            if (Iin.Length != 12)
                errors.Add(new KeyValuePair<string, string>("Iin", "Не верная длинна ИИН.{ошибка}"));

            if (string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(FirstName))
                errors.Add(new KeyValuePair<string, string>("Name", "Необходимо ввести фамилию и имя.{ошибка}"));

            if (Birthday.Date > DateTime.Now.Date)
                errors.Add(new KeyValuePair<string, string>("Birthday", "День рождения не может быть в будущем.{ошибка}"));

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
