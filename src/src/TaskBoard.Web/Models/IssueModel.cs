namespace GosuBoard.Web.Models
{
    public class IssueModel : EntityModel
    {
        public string Title { get; set; }

        public int BoardId { get; set; }

        public int? StateId { get; set; }
    }
}
