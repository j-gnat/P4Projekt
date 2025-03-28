using ChessGameLogic.Interfaces;
using ChessGameLogic.Enums;

namespace ChessGameLogic.Models
{
    internal abstract class Piece(PieceColor color, PieceType type, IMoveStrategy moveStrategy)
    {
        public PieceColor IsWhite { get; private set; } = color;
        public PieceType Type { get; private set; } = type;
        public IMoveStrategy MoveStrategy { get; set; } = moveStrategy;
    }
}
