using System.ComponentModel;
using ChessGameLogic.Enums;
using ChessGameLogic.Services.MoveStrategies;

namespace ChessGameLogic.Models
{
    internal class Queen(PieceColor color) : Piece(color, PieceType.Queen, new QueenMoveStrategy())
    {
    }
}
