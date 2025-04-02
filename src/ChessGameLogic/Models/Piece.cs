using ChessGameLogic.Interfaces;
using ChessGameLogic.Enums;

namespace ChessGameLogic.Models;

public class Piece(
    PieceColor color,
    PieceType type,
    List<IMoveStrategy> moveStrategy)
{
    public bool HasMoved { get; set; }
    public PieceColor Color { get; set; } = color;
    public PieceType Type { get; set; } = type;
    public List<IMoveStrategy> MoveStrategies { get; set; } = moveStrategy;

    public IEnumerable<Coordinate> GetValidMoves(Dictionary<Coordinate, Piece?> board, Coordinate from)
    {
        List<Coordinate> moves = [];
        foreach (IMoveStrategy strategy in MoveStrategies)
        {
            moves.AddRange(strategy.GetMoves(board, from));
        }
        return moves;
    }

    public bool IsCoordinateValidToMove(Dictionary<Coordinate, Piece?> board, Coordinate from, Coordinate to)
    {
        throw new NotImplementedException();
    }
}
