using GosuBoard.Web.Entities;
using GosuBoard.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace GosuBoard.Web.Mapping
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
                BoardId = issue.BoardId
            };
        }
    }
}
