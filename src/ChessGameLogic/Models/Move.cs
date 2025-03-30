using System.Security.Cryptography;

namespace ChessGameLogic.Models
{
    public struct Move((int row, int column) from,
        (int row, int column) to,
        Piece movedPiece,
        List<(Piece? piece, (int row, int column) position)> takenPieces)
    {
        public (int row, int column) From { get; set; } = from;
        public (int row, int column) To { get; set; } = to;
        public Piece MovedPiece { get; set; } = movedPiece;
        public List<(Piece? piece, (int row, int column) position)>? TakenPieces { get; set; } = takenPieces;
    }
}
