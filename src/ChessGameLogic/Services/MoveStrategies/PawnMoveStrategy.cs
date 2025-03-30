using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;
using ChessGameLogic.Models;

namespace ChessGameLogic.Services.MoveStrategies
{
    public class PawnMoveStrategy(MoveDirection moveDirection) : IMoveStrategy
    {
        public bool HasMoved { get; set; }

        private MoveDirection _moveDirection = moveDirection;

        public bool IsValidMove(Piece?[,] board, (int row, int column) from, (int row, int column) to)
        {
            throw new System.NotImplementedException();
        }

        public bool GetValidMoves(Piece?[,] board, (int row, int column) from, out (int row, int column)[] validMoves)
        {
            throw new System.NotImplementedException();
        }
    }
}
