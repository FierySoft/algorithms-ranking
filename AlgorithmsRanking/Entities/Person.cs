using System;
using System.Text;
using System.Linq;

namespace AlgorithmsRanking.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Фамилия Имя Отчество
        public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
 
        // Фамилия И.О.
        public string ShortName
        {
            get
            {
                var nameBuilder = new StringBuilder();

                nameBuilder.Append(LastName);

                if (!String.IsNullOrEmpty(FirstName))
                {
                    nameBuilder.Append($" {FirstName.ToUpper().First()}.");
                
                    if (!String.IsNullOrEmpty(LastName))
                    {
                        nameBuilder.Append($"{MiddleName.ToUpper().First()}.");
                    }
                }

                return nameBuilder.ToString();
            }
        }


        public Person()
        {

        }

        public Person(string lastName, string firstName, string middleName, string email, string phone)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            Email = email;
            Phone = phone;
        }
    }
}
