namespace AlgorithmsRanking.Entities
{
    public class DataSet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }


        public DataSet()
        {

        }

        public DataSet(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}
