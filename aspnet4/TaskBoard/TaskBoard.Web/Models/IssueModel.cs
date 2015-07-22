using Newtonsoft.Json;

namespace TaskBoard.Web.Models
{
    public class IssueModel : EntityModel
    {
        // camel casing in the JSON should really be handled through a 
        // custom contract resolver, but I can't find a way to configure
        // one with SignalR 3 beta 4. Perhaps this can be removed later on.

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("boardId")]
        public int BoardId { get; set; }

        [JsonProperty("stateId")]
        public int? StateId { get; set; }
    }
}
