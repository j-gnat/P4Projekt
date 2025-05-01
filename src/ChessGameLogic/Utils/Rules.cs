using ChessGameLogic.Enums;
using ChessGameLogic.Models;

namespace ChessGameLogic.Utils;

public static class Rules
{
    public static bool IsInCheck(Dictionary<Coordinate,Piece?> board, PieceColor playerColor)
    {
        Coordinate? kingPosition = board.Where(b => b.Value?.Type == PieceType.King && b.Value?.Color == playerColor)
            .Select(b => b.Key)
            .FirstOrDefault();
        bool isInCheck = board.Where(b => b.Value?.Color != playerColor)
            .Any(b => b.Value?.GetValidMoves(board, b.Key).Contains(kingPosition) == true);
        return isInCheck;
    }
    public static bool IsCheckMate(Piece?[,] board, PieceColor playerColor)
    {
        throw new System.NotImplementedException();
    }
    public static bool IsDraw(Piece?[,] board, PieceColor playerColor)
    {
        throw new System.NotImplementedException();
    }
}
