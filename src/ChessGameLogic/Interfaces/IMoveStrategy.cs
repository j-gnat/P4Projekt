using ChessGameLogic.Models;

namespace ChessGameLogic.Interfaces
{
    public interface IMoveStrategy
    {
        IEnumerable<Coordinate> GetMoves(Dictionary<Coordinate, Piece?> board, Coordinate from);
    }
}
