using ChessGameLogic.Enums;
using ChessGameLogic.Services.MoveStrategies;

namespace ChessGameLogic.Models
{
    internal class Bishop(PieceColor color) : Piece(color, PieceType.Bishop, new BishopMoveStrategy())
    {
    }
}
