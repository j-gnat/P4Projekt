using ChessGameLogic.Models;

namespace ChessGameLogic.Utils;

public static class PositionModifier
{
    public static Coordinate MoveUp(Coordinate position, int steps = 1)
    {
        return new Coordinate(position.row - steps, position.column);
    }
    public static Coordinate MoveDown(Coordinate position, int steps = 1)
    {
        return new Coordinate(position.row + steps, position.column);
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
}
