using GosuBoard.Web.Entities;
using GosuBoard.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace GosuBoard.Web.Mapping
{
    public static class IssueStateMappingExtensions
    {
        public static IList<IssueStateModel> ToModels(this IEnumerable<IssueState> entities)
        {
            return entities.Select(ToModel).ToList();
        }

        public static IssueStateModel ToModel(this IssueState issueState)
        {
            return new IssueStateModel()
            {
                Id = issueState.Id,
                Name = issueState.Name,
                BoardId = issueState.BoardId
            };
        }
    }
}
