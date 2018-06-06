using System;

namespace AlgorithmsRanking.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
        public string AvatarUri { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public DateTime RegisteredAt { get; set; }


        public Account()
        {

        }

        public Account(int personId, string userName, string password, string role, string avatar)
        {
            PersonId = personId;
            UserName = userName;
            Password = password;
            Role = role;
            AvatarUri = avatar;
            RegisteredAt = DateTime.Now;
        }
    }
}
