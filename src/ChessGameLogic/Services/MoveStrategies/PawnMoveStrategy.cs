using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;
using ChessGameLogic.Models;

namespace ChessGameLogic.Services.MoveStrategies
{
    public class PawnMoveStrategy(MoveDirection moveDirection) : IMoveStrategy
    {
        public bool HasMoved { get; set; }

        private MoveDirection _moveDirection = moveDirection;

        public bool IsValidMove(Piece?[,] board, Coordinate from, Coordinate to)
        {
            throw new System.NotImplementedException();
        }

        public bool GetValidMoves(Piece?[,] board, Coordinate from, out Coordinate[] validMoves)
        {
            throw new System.NotImplementedException();
        }
    }
}
