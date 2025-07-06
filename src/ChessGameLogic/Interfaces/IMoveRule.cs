using ChessGameLogic.Models;

namespace ChessGameLogic.Interfaces;

internal interface IMoveRule
{
    bool IsValidMove(Coordinate from, Coordinate to, Dictionary<Coordinate, Piece?> board);
}
