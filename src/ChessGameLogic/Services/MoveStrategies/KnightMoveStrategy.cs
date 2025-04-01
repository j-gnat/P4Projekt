using ChessGameLogic.Interfaces;
using ChessGameLogic.Models;

namespace ChessGameLogic.Services.MoveStrategies
{
    public class KnightMoveStrategy : IMoveStrategy
    {
        public bool HasMoved { get; set; }
        public bool IsValidMove(Piece?[,] board, Coordinate from, Coordinate to)
        {
            throw new System.NotImplementedException();
        }

        public bool GetMoves(Dictionary<Coordinate, Piece?> board, Coordinate from, out IEnumerable<Coordinate> validMoves)
        {
            throw new System.NotImplementedException();
        }
    }
}
