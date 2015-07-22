using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace TaskBoard.Web.Models
{
    public class EntityModel
    {
        public EntityModel()
        {
            Links = new List<LinkModel>();
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("links")]
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
