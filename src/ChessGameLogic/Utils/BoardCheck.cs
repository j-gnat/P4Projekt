using ChessGameLogic.Enums;
using ChessGameLogic.Models;

namespace ChessGameLogic.Utils;

public static class BoardCheck
{
    public static bool IsEmpty(Dictionary<Coordinate, Piece?> board, Coordinate position)
    {
        board.TryGetValue(position, out Piece? piece);
        return piece == null;
    }

    public static bool IsEnemyOrNull(Dictionary<Coordinate, Piece?> board, Coordinate position, PieceColor color)
    {
        board.TryGetValue(position, out Piece? piece);
        return piece?.Color != color;
    }
}
