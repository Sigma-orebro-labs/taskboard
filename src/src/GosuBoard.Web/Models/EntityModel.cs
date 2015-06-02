using System.Collections.Generic;
using System.Linq;

namespace GosuBoard.Web.Models
{
    public class EntityModel
    {
        public EntityModel()
        {
            Links = new List<LinkModel>();
        }

        public int Id { get; set; }

        public IList<LinkModel> Links { get; private set; }

        public void AddLink(LinkModel link)
        {
            Links.Add(link);
        }

        public string GetSelfHref()
        {
            return Links.First(x => x.Rel == "self").Href;
        }
    }
}
