using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;
using ChessGameLogic.Models;
using ChessGameLogic.Utils;

namespace ChessGameLogic.Services.MoveStrategies;

public class MoveKnightStyle(IEnumerable<MoveDirection> directions) : IMoveStrategy
{
    private readonly Dictionary<MoveDirection, Func<Coordinate,Coordinate>> _directionsTranslator = new()
    {
        { MoveDirection.UpLeft, (from) => new Coordinate(from.row + 2, from.column - 1) },
        { MoveDirection.UpRight, (from) => new Coordinate(from.row + 2, from.column + 1) },
        { MoveDirection.DownLeft, (from) => new Coordinate(from.row - 2, from.column - 1) },
        { MoveDirection.DownRight, (from) => new Coordinate(from.row - 2, from.column + 1) },
        { MoveDirection.LeftUp, (from) => new Coordinate(from.row + 1, from.column - 2) },
        { MoveDirection.LeftDown, (from) => new Coordinate(from.row - 1, from.column - 2) },
        { MoveDirection.RightUp, (from) => new Coordinate(from.row + 1, from.column + 2) },
        { MoveDirection.RightDown, (from) => new Coordinate(from.row -1, from.column + 2) }
    };
    private readonly IEnumerable<MoveDirection> _directions = directions;
    public IEnumerable<Coordinate> GetMoves(Dictionary<Coordinate, Piece?> board, Coordinate from)
    {
        Piece piece = board[from] ?? throw new InvalidOperationException("There is no piece on the given position.");
        List<Coordinate> result = [];
        foreach (MoveDirection direction in _directions)
        {
            if (!_directionsTranslator.TryGetValue(direction, out Func<Coordinate, Coordinate>? moveFunction))
            {
                continue;
            }
            Coordinate currentPosition = moveFunction(from);
            if (board.ContainsKey(currentPosition) && BoardCheck.IsEnemyOrNull(board, currentPosition, piece.Color))
            {
                result.Add(currentPosition);
            }
        }

        return result;
    }
}
