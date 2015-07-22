using TaskBoard.Domain.Entities;
using TaskBoard.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace TaskBoard.Web.Mapping
{
    public static class IssueMappingExtensions
    {
        public static IList<IssueModel> ToModels(this IEnumerable<Issue> entities)
        {
            return entities.Select(ToModel).ToList();
        }

        public static IssueModel ToModel(this Issue issue)
        {
            return new IssueModel()
            {
                Id = issue.Id,
                Title = issue.Title,
                Description = issue.Description,
                BoardId = issue.BoardId,
                StateId = issue.StateId
            };
        }
    }
}
