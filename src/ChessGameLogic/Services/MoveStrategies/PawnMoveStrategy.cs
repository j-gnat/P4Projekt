using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;
using ChessGameLogic.Models;
using ChessGameLogic.Utils;

namespace ChessGameLogic.Services.MoveStrategies
{
    public class PawnMoveStrategy(MoveDirection moveDirection) : IMoveStrategy
    {
        private static readonly Dictionary<MoveDirection, Func<Coordinate, int, Coordinate>> s_standardMoveDictionary = new Dictionary<MoveDirection, Func<Coordinate, int, Coordinate>>
        {
            { MoveDirection.Up, PositionModifier.MoveUp },
            { MoveDirection.Down, PositionModifier.MoveDown },
            { MoveDirection.Left, PositionModifier.MoveLeft },
            { MoveDirection.Right, PositionModifier.MoveRight },
        };

        private MoveDirection _moveDirection = moveDirection;

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
