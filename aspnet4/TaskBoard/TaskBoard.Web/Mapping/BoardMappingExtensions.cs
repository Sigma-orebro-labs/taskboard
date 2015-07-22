using TaskBoard.Domain.Entities;
using TaskBoard.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace TaskBoard.Web.Mapping
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
