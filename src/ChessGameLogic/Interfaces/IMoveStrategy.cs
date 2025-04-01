using ChessGameLogic.Models;

namespace ChessGameLogic.Interfaces
{
    public interface IMoveStrategy
    {
        /// <summary>
        /// Get all possible moves for a piece on the board
        /// </summary>
        /// <param name="board"></param>
        /// <param name="from"></param>
        /// <param name="possibleMoves"></param>
        /// <returns>In case the piece is not found on the from coordinate, return empty list and false as a result.
        /// Otherwise returns true</returns>
        bool GetMoves(Dictionary<Coordinate, Piece?> board, Coordinate from, out IEnumerable<Coordinate> possibleMoves);
    }
}
