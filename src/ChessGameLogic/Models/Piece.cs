using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;
using ChessGameLogic.Models.Rules;

namespace ChessGameLogic.Models;

public class Piece
{
    public bool HasMoved { get; set; }
    public required PieceColor Color { get; init; }
    public required PieceType Type { get; init; }
    public required List<IMoveStrategy> MoveStrategy { get; init; }

    public IEnumerable<Coordinate> GetMoves(Dictionary<Coordinate, Piece?> board, Coordinate from)
    {
        List<Coordinate> moves = [];
        foreach (IMoveStrategy strategy in MoveStrategy)
        {
            var movesToAdd = strategy.GetMoves(board, from)
                .Where(m => !moves.Contains(m)).ToList();
            moves.AddRange(movesToAdd);
        }
        return moves;
    }

    public bool IsCoordinateValidToMove(Dictionary<Coordinate, Piece?> board, Coordinate from, Coordinate to)
    {
        ///TODO: Check if the move is valid according to the rules of chess.
        return true;
    }

    public override string ToString() => $"Piece {{Type: {Type} Color: {Color} HasMoved: {HasMoved}}}";
}
