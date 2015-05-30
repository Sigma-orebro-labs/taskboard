using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GosuBoard.Web.Models
{
    public class IssueModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int BoardId { get; set; }

        public LinkModel BoardLink { get; set; }
    }
}
