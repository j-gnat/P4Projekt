using System.ComponentModel;
using ChessGameLogic.Enums;
using ChessGameLogic.Services.MoveStrategies;

namespace ChessGameLogic.Models
{
    internal class Knight(PieceColor color) : Piece(color, PieceType.Knight, new KnightMoveStrategy())
    {
    }
}
