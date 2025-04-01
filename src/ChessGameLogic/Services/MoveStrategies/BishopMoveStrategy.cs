using System.Collections.Generic;
using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;
using ChessGameLogic.Models;
using ChessGameLogic.Utils;

namespace ChessGameLogic.Services.MoveStrategies;

public class BishopMoveStrategy : IMoveStrategy
{
    public bool GetMoves(Dictionary<Coordinate, Piece?> board, Coordinate startPosition, out IEnumerable<Coordinate> possibleMoves)
    {
        possibleMoves = new List<Coordinate>();
        if (board.TryGetValue(startPosition, out Piece? piece) || piece is null)
        {
            return false;
        }

        possibleMoves = PositionModifier.GetAllDiagonalCoordinates(board.Keys, startPosition);

        return true;
    }
}
