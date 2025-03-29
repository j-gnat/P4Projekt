using ChessGameLogic.Interfaces;
using ChessGameLogic.Enums;

namespace ChessGameLogic.Models
{
    public class Piece(PieceColor color, PieceType type, IMoveStrategy moveStrategy)
    {
        public PieceColor Color { get; internal set; } = color;
        public PieceType Type { get; set; } = type;
        public IMoveStrategy MoveStrategy { get; set; } = moveStrategy;
    }
}
