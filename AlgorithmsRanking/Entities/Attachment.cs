namespace AlgorithmsRanking.Entities
{
    public class Attachment
    {
        public int Id { get; set; }
        public int DataSetId { get; set; }
        public string Url { get; set; }


        public Attachment()
        {

        }

        public Attachment(int dataSetId, string url)
        {
            DataSetId = dataSetId;
            Url = url;
        }
    }
}
