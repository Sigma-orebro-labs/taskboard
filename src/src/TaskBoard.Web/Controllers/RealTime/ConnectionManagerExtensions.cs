using TaskBoard.Web.Models;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskBoard.Web.Controllers.RealTime
{
    public static class ConnectionManagerExtensions
    {
        public static void BroadcastAddIssue(this IConnectionManager connectionManager, IssueModel issue)
        {
            var boardHub = connectionManager.GetHubContext<BoardHub>();

            boardHub.Clients.Group(issue.BoardId.ToString()).addIssue(issue);
        }

        public static void BroadcastDeleteIssue(this IConnectionManager connectionManager, IssueModel issue)
        {
            var boardHub = connectionManager.GetHubContext<BoardHub>();

            boardHub.Clients.Group(issue.BoardId.ToString()).deleteIssue(issue.Id);
        }
    }
}
