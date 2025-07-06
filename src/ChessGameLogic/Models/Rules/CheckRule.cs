using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;

namespace ChessGameLogic.Models.Rules;

internal class CheckRule: IMoveRule
{
    /// <summary>
    /// It checks if after current player move any other player can catch king
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="board"></param>
    /// <returns></returns>
    public bool IsValidMove(Coordinate from, Coordinate to, Dictionary<Coordinate, Piece?> board)
    {
        if (!board.TryGetValue(from, out var currentPlayerPiece))
        {
            return false;
        }

        PieceColor playerColor = currentPlayerPiece!.Color;

        var kingPosition = board.Where(b => b.Value?.Type == PieceType.King && b.Value?.Color == playerColor)
            .Select(b => b.Key);

        return kingPosition.Count() > 0;
    }
}
