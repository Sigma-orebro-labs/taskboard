using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBoard.Web.Models
{
    public class IssueStateModel : EntityModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("boardId")]
        public int BoardId { get; set; }
    }
}
