using System.ComponentModel;
using ChessGameLogic.Enums;
using ChessGameLogic.Services.MoveStrategies;

namespace ChessGameLogic.Models
{
    internal class Pawn(PieceColor color) : Piece(color, PieceType.Pawn, new PawnMoveStrategy())
    {
    }
}
