namespace GosuBoard.Web.Entities
{
    public class Issue
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int BoardId { get; set; }
    }
}