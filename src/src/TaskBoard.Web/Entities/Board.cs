using System.Collections.Generic;

namespace TaskBoard.Web.Entities
{
    public class Board : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}