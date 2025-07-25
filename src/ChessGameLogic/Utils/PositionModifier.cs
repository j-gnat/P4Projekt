﻿using ChessGameLogic.Enums;
using ChessGameLogic.Models;

namespace ChessGameLogic.Utils;

public static class PositionModifier
{
    public static Coordinate MoveUp(Coordinate position, int steps = 1)
    {
        return new Coordinate(position.row + steps, position.column);
    }
    public static Coordinate MoveDown(Coordinate position, int steps = 1)
    {
        return new Coordinate(position.row - steps, position.column);
    }
    public static Coordinate MoveLeft(Coordinate position, int steps = 1)
    {
        return new Coordinate(position.row, position.column - steps);
    }
    public static Coordinate MoveRight(Coordinate position, int steps = 1)
    {
        return new Coordinate(position.row, position.column + steps);
    }
    public static Coordinate MoveUpLeft(Coordinate position, int steps = 1)
    {
        return new Coordinate(position.row - steps, position.column - steps);
    }
    public static Coordinate MoveUpRight(Coordinate position, int steps = 1)
    {
        return new Coordinate(position.row - steps, position.column + steps);
    }
    public static Coordinate MoveDownLeft(Coordinate position, int steps = 1)
    {
        return new Coordinate(position.row + steps, position.column - steps);
    }
    public static Coordinate MoveDownRight(Coordinate position, int steps = 1)
    {
        return new Coordinate(position.row + steps, position.column + steps);
    }
    public static IEnumerable<Coordinate> GetAllDiagonalCoordinates(IEnumerable<Coordinate> coordinates, Coordinate currentPosition)
    {
        List<Coordinate> result = [.. coordinates
            .Where(coord => Math.Abs(currentPosition.row - coord.row) == Math.Abs(currentPosition.column - coord.column))
            .Select(coord => coord)];
        return result;
    }
    public static IEnumerable<Coordinate> GetAllHorizontalAndVerticalCoordinates(IEnumerable<Coordinate> coordinates, Coordinate currentPosition)
    {
        List<Coordinate> result = [.. coordinates
            .Where(coord => currentPosition.row == coord.row || currentPosition.column == coord.column)
            .Select(coord => coord)];
        return result;
    }

    public static bool GetLinearMoveFunction(MoveDirection direction, out Func<Coordinate, int, Coordinate>? func)
    {
        func = direction switch
        {
            MoveDirection.Up => MoveUp,
            MoveDirection.Down => MoveDown,
            MoveDirection.Left => MoveLeft,
            MoveDirection.Right => MoveRight,
            MoveDirection.UpLeft => MoveUpLeft,
            MoveDirection.UpRight => MoveUpRight,
            MoveDirection.DownLeft => MoveDownLeft,
            MoveDirection.DownRight => MoveDownRight,
            _ => null,
        };
        return func != null;
    }
}
