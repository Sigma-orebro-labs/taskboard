using GosuBoard.Web.Entities;
using GosuBoard.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace GosuBoard.Web.Mapping
{
    public static class BoardMappingExtensions
    {
        public static IList<BoardModel> ToModels(this IEnumerable<Board> entities)
        {
            return entities.Select(ToModel).ToList();
        }

        public static BoardModel ToModel(this Board board)
        {
            return new BoardModel()
            {
                Id = board.Id,
                Name = board.Name,
                Description = board.Description,
            };
        }
    }
}
