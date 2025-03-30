using ChessGameLogic.Models;

namespace ChessGameLogic.Interfaces
{
    public interface IMoveStrategy
    {
        public bool HasMoved { get; set; }
        bool IsValidMove(Piece?[,] board, (int row, int column) from, (int row, int column) to);
        bool GetValidMoves(Piece?[,] board, (int row, int column) from, out (int row, int column)[] validMoves);
    }
}
