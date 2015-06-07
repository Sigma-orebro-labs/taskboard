namespace GosuBoard.Web.Entities
{
    public class Issue : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int BoardId { get; set; }

        public int? StateId { get; set; }
        
    }
}