namespace ChessGameLogic.Models;

public struct Move(Coordinate from,
    Coordinate to,
    Piece movedPiece,
    IEnumerable<(Piece? piece, Coordinate position)> takenPieces)
{
    public Coordinate From { get; set; } = from;
    public Coordinate To { get; set; } = to;
    public Piece MovedPiece { get; set; } = movedPiece;
    public IEnumerable<(Piece? piece, Coordinate position)>? TakenPieces { get; set; } = takenPieces;
}
