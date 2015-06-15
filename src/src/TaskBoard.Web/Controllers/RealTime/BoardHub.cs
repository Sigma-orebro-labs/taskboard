using GosuBoard.Web.Models;
using Microsoft.AspNet.SignalR;

namespace GosuBoard.Web.Controllers.RealTime
{
    public class BoardHub : Hub
    {
        public void BroadcastAddIssue(IssueModel issue)
        {
            Clients.Others.addIssue(issue);
        }
    }
}
