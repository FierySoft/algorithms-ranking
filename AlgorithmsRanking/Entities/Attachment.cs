using System;

namespace AlgorithmsRanking.Entities
{
    public class Attachment
    {
        public int Id { get; set; }
        public int DataSetId { get; set; }
        public string Url { get; set; }
        public long ContentLength { get; set; }
        public string Name { get; set; }


        public Attachment()
        {

        }

        public Attachment(int dataSetId, string url)
            : this(dataSetId, url, 0, new Guid().ToString())
        {

        }

        public Attachment(int dataSetId, string url, long length, string name)
        {
            DataSetId = dataSetId;
            Url = url;
            ContentLength = length;
            Name = name;
        }
    }
}
