using ChessGameLogic.Models;

namespace ChessGameLogic.Interfaces
{
    public interface IMoveStrategy
    {
        List<string> GetPossibleMoves(Piece piece, Board board);
    }
}
