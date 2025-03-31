using ChessGameLogic.Enums;
using ChessGameLogic.Models;

namespace ChessGameLogic.Utils
{
    public static class BoardCheck
    {
        public static bool IsInBoardRange(Piece?[,] board, Coordinate position)
        {
            return position.row >= 0 && position.row < board.GetLength(0) && position.column >= 0 && position.column < board.GetLength(1);
        }

        public static bool IsEmpty(Piece?[,] board, Coordinate position)
        {
            return board[position.row, position.column] == null;
        }

        public static bool IsEnemy(Piece?[,] board, Coordinate position, PieceColor color)
        {
            return board[position.row, position.column]?.Color != color;
        }
    }
}
