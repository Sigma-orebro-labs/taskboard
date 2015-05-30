namespace GosuBoard.Web.Models
{
    public class BoardModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public LinkModel IssuesLink { get; set; }
    }
}
