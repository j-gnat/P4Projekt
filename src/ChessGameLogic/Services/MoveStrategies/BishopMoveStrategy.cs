using System.Collections.Generic;
using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;
using ChessGameLogic.Models;
using ChessGameLogic.Utils;

namespace ChessGameLogic.Services.MoveStrategies;

public class BishopMoveStrategy : IMoveStrategy
{
    public bool HasMoved { get; set; }
    public bool IsValidMove(Piece?[,] board, Coordinate from, Coordinate to)
    {
        GetValidMoves(board, from, out IEnumerable<Coordinate> validMoves);
        return validMoves.Any(move => move == to);
    }

    public bool GetValidMoves(Piece?[,] board, Coordinate from, out IEnumerable<Coordinate> validMoves)
    {
        Piece piece = board[from.row, from.column] ?? throw new NullReferenceException();
        IEnumerable<Coordinate> possibleMoves = GetPossibleMoves(board, from);
        validMoves = possibleMoves;
        if (Rules.IsInCheck(board, piece.Color))
        {
            
        }

        return true;
    }

    private static IEnumerable<Coordinate> GetPossibleMoves(Piece?[,] board, Coordinate startPosition)
    {
        Piece piece = board[startPosition.row, startPosition.column] ?? throw new NullReferenceException();
        IEnumerable<Func<Coordinate, int, Coordinate>> directionModifiers = new List< Func<Coordinate, int, Coordinate>> {
            { PositionModifier.MoveUpLeft },
            { PositionModifier.MoveUpRight },
            { PositionModifier.MoveDownLeft },
            { PositionModifier.MoveDownRight }
        };

        List<Coordinate> result = new();

        foreach (Func<Coordinate, int, Coordinate> directionModifier in directionModifiers)
        {
            int steps = 0;
            while (true)
            {
                steps++;
                Coordinate position = directionModifier(startPosition, steps);
                if (!BoardCheck.IsInBoardRange(board, position))
                {
                    break;
                }
                if (!BoardCheck.IsEmpty(board, position) && !BoardCheck.IsEnemy(board, position, piece.Color))
                {
                    break;
                }
                if (BoardCheck.IsEnemy(board, position, piece.Color))
                {
                    result.Add(position);
                    break;
                }
                result.Add(position);
            }
        }
        return result;
    }
}
