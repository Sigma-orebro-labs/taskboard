using Newtonsoft.Json;

namespace TaskBoard.Web.Models
{
    public class LinkModel
    {
        public LinkModel() { }

        public LinkModel(string rel, string prompt, string href)
        {
            Rel = rel.ToLower();
            Prompt = prompt;
            Href = href;
        }

        [JsonProperty("rel")]
        public readonly string Rel;

        [JsonProperty("href")]
        public readonly string Href;

        [JsonProperty("prompt")]
        public readonly string Prompt;
    }
}
