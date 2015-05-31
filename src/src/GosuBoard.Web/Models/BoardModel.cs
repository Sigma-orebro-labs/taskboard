using System.Collections.Generic;

namespace GosuBoard.Web.Models
{
    public class BoardModel
    {
        public BoardModel()
        {
            Links = new List<LinkModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IList<LinkModel> Links { get; private set; }
    }
}
