using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;
using ChessGameLogic.Models;

namespace ChessGameLogic.Services.MoveStrategies;

// TODO: Give somehow information about the last move to this strategy
public class MoveEnPassant(IEnumerable<MoveDirection> directions) : IMoveStrategy
{
    private readonly IEnumerable<MoveDirection> _directions = directions;
    public IEnumerable<Coordinate> GetMoves(Dictionary<Coordinate, Piece?> board, Coordinate from)
    {
        throw new System.NotImplementedException();
    }
}
