namespace AlgorithmsRanking.Entities
{
    public class DataSet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int AttributesCount { get; set; }
        public int StringsCount { get; set; }

        public Attachment[] Files { get; set; }
        public int FilesCount => Files != null ? Files.Length : 0;


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
