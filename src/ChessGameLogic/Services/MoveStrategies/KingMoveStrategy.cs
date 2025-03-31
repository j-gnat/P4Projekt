using ChessGameLogic.Interfaces;
using ChessGameLogic.Models;

namespace ChessGameLogic.Services.MoveStrategies
{
    public class KingMoveStrategy : IMoveStrategy
    {
        public bool HasMoved { get; set; }
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
