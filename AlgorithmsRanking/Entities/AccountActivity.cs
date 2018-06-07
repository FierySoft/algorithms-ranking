using System;

namespace AlgorithmsRanking.Entities
{
    public class AccountActivity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string IpAddress { get; set; }
        public string Operation { get; set; }
        public DateTime At { get; set; }
    }
}
