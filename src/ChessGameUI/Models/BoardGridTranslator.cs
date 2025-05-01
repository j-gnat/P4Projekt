using Avalonia.Controls;
using System;
using ChessGameLogic.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChessGameUI.Models;

internal class BoardGridTranslator
{
    private readonly Board _board;
    private readonly int _minRow;
    private readonly int _minColumn;
    private readonly int _maxRow;
    private readonly int _maxColumn;
    private int _rowsModifier => -_minRow;
    private int _columnsModifier => -_minColumn;

    public BoardGridTranslator(Board board)
    {
        _board = board;
        Dictionary<Coordinate, Piece?> boardDictionary = _board.BoardTab;
        _minRow = boardDictionary.Keys.Min(r => r.row);
        _minColumn = boardDictionary.Keys.Min(r => r.column);
        _maxRow = boardDictionary.Keys.Max(r => r.row) + _rowsModifier;
        _maxColumn = boardDictionary.Keys.Max(r => r.column) + _columnsModifier;
        RowsCount = _maxRow - _minRow;
        ColumnsCount = _maxColumn - _minColumn;
        IsFlipped = true;
    }

    public int RowsCount { get; }
    public int ColumnsCount { get; }
    public bool IsFlipped { get; set; }

    /// <summary>
    /// Translate the coordinate from the grid to the board and opposite.
    /// In Board row number 0 represents the bottom, but in grid row number 0 is on the top.
    /// This is the reason there is need to use this translator.
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    public Coordinate TranslateCoordinte(Grid grid, Coordinate coordinate)
    {
        int row = IsFlipped ? Math.Abs(grid.RowDefinitions.Count - coordinate.row - 1) + _rowsModifier : coordinate.row + _rowsModifier;
        int column = IsFlipped ? coordinate.column + _columnsModifier : Math.Abs(coordinate.column - grid.ColumnDefinitions.Count + 1) + _columnsModifier;
        return new Coordinate(row, column);
    }

}
