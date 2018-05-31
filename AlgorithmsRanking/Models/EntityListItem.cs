namespace AlgorithmsRanking.Models
{
    public class EntityListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public EntityListItem()
        {

        }

        public EntityListItem(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
