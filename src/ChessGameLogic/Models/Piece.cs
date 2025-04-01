using ChessGameLogic.Interfaces;
using ChessGameLogic.Enums;

namespace ChessGameLogic.Models;

public class Piece(
    PieceColor color,
    PieceType type,
    IMoveStrategy moveStrategy)
{
    public bool HasMoved { get; set; }
    public PieceColor Color { get; set; } = color;
    public PieceType Type { get; set; } = type;
    public IMoveStrategy MoveStrategy { get; set; } = moveStrategy;

    public IEnumerable<Coordinate> GetValidMoves(Dictionary<Coordinate, Piece?> board, Coordinate from)
    {
        MoveStrategy.GetMoves(board, from, out IEnumerable<Coordinate> moves);
        return moves;
    }

    public bool IsCoordinateValidToMove(Dictionary<Coordinate, Piece?> board, Coordinate from, Coordinate to)
    {
        throw new NotImplementedException();
    }
}
