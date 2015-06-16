namespace TaskBoard.Web.Entities
{
    public class IssueState : Entity
    {
        public string Name { get; set; }

        public int BoardId { get; set; }
    }
}