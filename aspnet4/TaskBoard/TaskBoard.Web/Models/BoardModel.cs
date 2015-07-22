using System.Collections.Generic;

namespace TaskBoard.Web.Models
{
    public class BoardModel : EntityModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
