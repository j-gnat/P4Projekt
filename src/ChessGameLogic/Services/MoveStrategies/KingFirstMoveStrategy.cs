using ChessGameLogic.Interfaces;
using ChessGameLogic.Models;

namespace ChessGameLogic.Services.MoveStrategies;

public class KingFirstMoveStrategy :IMoveStrategy
{
    public bool GetMoves(Dictionary<Coordinate, Piece?> board, Coordinate startPosition, out IEnumerable<Coordinate> possibleMoves)
    {
        possibleMoves = new List<Coordinate>();
        if (board.TryGetValue(startPosition, out Piece? piece) || piece is null)
        {
            return false;
        }
        possibleMoves = new List<Coordinate>
        {
            new Coordinate(startPosition.row, startPosition.column + 2),
            new Coordinate(startPosition.row, startPosition.column - 2)
        };
        return true;
    }
}
