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
            connectionManager.GetGroup(issue.BoardId).addIssue(issue);
        }

        public static void BroadcastUpdateIssue(this IConnectionManager connectionManager, IssueModel issue)
        {
            connectionManager.GetGroup(issue.BoardId).updateIssue(issue);
        }

        public static void BroadcastDeleteIssue(this IConnectionManager connectionManager, IssueModel issue)
        {
            connectionManager.GetGroup(issue.BoardId).deleteIssue(issue.Id);
        }

        public static void BroadcastAddState(this IConnectionManager connectionManager, IssueStateModel issueState)
        {
            connectionManager.GetGroup(issueState.BoardId).addState(issueState);
        }

        public static void BroadcastDeleteState(this IConnectionManager connectionManager, IssueStateModel issueState)
        {
            connectionManager.GetGroup(issueState.BoardId).deleteState(issueState.Id);
        }

        private static dynamic GetGroup(this IConnectionManager connectionManager, int boardId)
        {
            var boardHub = connectionManager.GetHubContext<BoardHub>();

            return boardHub.Clients.Group(boardId.ToString());
        }
    }
}
