using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;
using ChessGameLogic.Models;
using ChessGameLogic.Utils;

namespace ChessGameLogic.Services.MoveStrategies;

public class MoveInLine(IEnumerable<MoveDirection> directions, int maxSteps = -1) : IMoveStrategy
{
    private readonly IEnumerable<MoveDirection> _directions = directions;
    private readonly int _maxSteps = maxSteps;
    public IEnumerable<Coordinate> GetMoves(Dictionary<Coordinate, Piece?> board, Coordinate from)
    {
        Piece? piece = board[from] ?? throw new InvalidOperationException("There is no piece on the given position.");
        List<Coordinate> result = [];
        foreach (MoveDirection direction in _directions)
        {
            if (!PositionModifier.GetLinearMoveFunction(direction, out Func<Coordinate, int, Coordinate>? moveFunction))
            {
                continue;
            }

            int maxSteps = ValidateMaxSteps(board, from, direction);

            for (int i = 1; i <= maxSteps; i++)
            {
                Coordinate currentPosition = moveFunction!(from, i);

                // I'm allowing that board have gaps, so I continue if the position is not in the board
                if (!board.ContainsKey(currentPosition))
                {
                    continue;
                }
                else if (BoardCheck.IsEmpty(board, currentPosition))
                {
                    result.Add(currentPosition);
                }
                else if (BoardCheck.IsEnemy(board[currentPosition]!, piece))
                {
                    result.Add(currentPosition);
                    break;
                }
                else
                {
                    break;
                }
            }
        }

        return result;
    }

    private int ValidateMaxSteps(Dictionary<Coordinate, Piece?> board, Coordinate from, MoveDirection direction)
    {
        int boardHeight = board.Keys.Max(c => c.row);
        int boardWidth = board.Keys.Max(c => c.column);

        int result = direction switch
        {
            MoveDirection.Up => boardHeight - from.row,
            MoveDirection.Down => from.row,
            MoveDirection.Left => from.column,
            MoveDirection.Right => boardWidth - from.column,
            MoveDirection.UpLeft => Math.Min(from.row, from.column),
            MoveDirection.UpRight => Math.Min(from.row, boardWidth - from.column),
            MoveDirection.DownLeft => Math.Min(boardHeight - from.row, from.column),
            MoveDirection.DownRight => Math.Min(boardHeight - from.row, boardWidth - from.column),
            _ => 0,
        };

        if (_maxSteps == -1)
        {
            return result;
        }

        return Math.Min(result, _maxSteps);
    }


}
