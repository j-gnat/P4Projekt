using ChessGameLogic.Enums;
using ChessGameLogic.Services.MoveStrategies;

namespace ChessGameLogic.Models
{
    internal class King(PieceColor color) : Piece(color, PieceType.King, new KingMoveStrategy())
    {
    }
}
