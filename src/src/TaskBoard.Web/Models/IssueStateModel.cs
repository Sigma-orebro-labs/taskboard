﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBoard.Web.Models
{
    public class IssueStateModel : EntityModel
    {
        public string Name { get; set; }

        public int BoardId { get; set; }
    }
}
