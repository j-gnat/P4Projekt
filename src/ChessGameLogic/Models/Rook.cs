using System.ComponentModel;
using ChessGameLogic.Enums;
using ChessGameLogic.Services.MoveStrategies;

namespace ChessGameLogic.Models
{
    internal class Rook(PieceColor color) : Piece(color, PieceType.Rook, new QueenMoveStrategy())
    {
    }
}
