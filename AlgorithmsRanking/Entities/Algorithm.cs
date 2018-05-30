namespace AlgorithmsRanking.Entities
{
    public class Algorithm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }


        public Algorithm()
        {

        }

        public Algorithm(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}
