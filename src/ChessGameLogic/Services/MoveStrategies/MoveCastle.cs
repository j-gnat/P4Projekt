using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;
using ChessGameLogic.Models;

namespace ChessGameLogic.Services.MoveStrategies;

public class MoveCastle(IEnumerable<MoveDirection> directions, IEnumerable<PieceType> castlingPieces) : IMoveStrategy
{
    private readonly IEnumerable<MoveDirection> _directions = directions;
    private readonly IEnumerable<PieceType> _pieciesWithWhichCanCaste = castlingPieces;
    public IEnumerable<Coordinate> GetMoves(Dictionary<Coordinate, Piece?> board, Coordinate from)
    {
        throw new System.NotImplementedException();
    }
}
