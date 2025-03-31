using ChessGameLogic.Models;

namespace ChessGameLogic.Interfaces
{
    public interface IMoveStrategy
    {
        public bool HasMoved { get; set; }
        bool IsValidMove(Piece?[,] board, Coordinate from, Coordinate to);
        bool GetValidMoves(Piece?[,] board, Coordinate from, out IEnumerable<Coordinate> validMoves);
    }
}
