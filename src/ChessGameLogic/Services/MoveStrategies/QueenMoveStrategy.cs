using ChessGameLogic.Interfaces;
using ChessGameLogic.Models;
using ChessGameLogic.Utils;

namespace ChessGameLogic.Services.MoveStrategies
{
    public class QueenMoveStrategy : IMoveStrategy
    {
        public bool GetMoves(Dictionary<Coordinate, Piece?> board, Coordinate startPosition, out IEnumerable<Coordinate> possibleMoves)
        {
            List<Coordinate> moves = new List<Coordinate>();
            possibleMoves = moves;
            if (board.TryGetValue(startPosition, out Piece? piece) || piece is null)
            {
                return false;
            }
            moves.AddRange(PositionModifier.GetAllDiagonalCoordinates(board.Keys, startPosition));
            moves.AddRange(PositionModifier.GetAllHorizontalAndVerticalCoordinates(board.Keys, startPosition));

            return true;
        }
    }
}
