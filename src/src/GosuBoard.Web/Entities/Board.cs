using System.Collections.Generic;

namespace GosuBoard.Web.Entities
{
    public class Board
    {
        public Board()
        {
            Issues = new List<Issue>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IList<Issue> Issues { get; private set; }
    }
}