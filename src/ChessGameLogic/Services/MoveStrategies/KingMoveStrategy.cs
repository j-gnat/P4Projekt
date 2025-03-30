using ChessGameLogic.Interfaces;
using ChessGameLogic.Models;

namespace ChessGameLogic.Services.MoveStrategies
{
    public class KingMoveStrategy : IMoveStrategy
    {
        private bool _hasMoved;
        public bool IsValidMove(Piece?[,] board, (int row, int column) from, (int row, int column) to)
        {
            throw new System.NotImplementedException();
        }

        public bool GetValidMoves(Piece?[,] board, (int row, int column) from, out (int row, int column)[] validMoves)
        {
            throw new System.NotImplementedException();
        }

        public bool MovePiece(Piece?[,] board, (int row, int column) from, (int row, int column) to)
        {
            throw new System.NotImplementedException();
        }
    }
}
