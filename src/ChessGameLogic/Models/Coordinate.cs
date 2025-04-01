namespace ChessGameLogic.Models;

public record Coordinate(int row, int column)
{
    public int row { get; private set; } = row;
    public int column { get; private set; } = column;
    public (int row, int column) ToTuple() => (row, column);
}
