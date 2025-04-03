using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using ChessGameLogic.Models;
using ChessGameLogic.Services;
using ChessGameLogic.Enums;
using Avalonia.Media;
using Avalonia;
using System;

namespace ChessGameUI.Views;

public partial class MainWindow : Window
{
    private ChessGame _gameService;
    private IBrush _brushLightCells = Brushes.White;
    private IBrush _brushDarkCells = Brushes.Brown;
    private IBrush _brushBorder = Brushes.Black;
    private bool _flipBoard = false;
    private Dictionary<Coordinate, Coordinate> _gridBoardPiecePosiotions = [];
    public MainWindow()
    {
        InitializeComponent();
        _gameService = new ChessGame();
        InitializeGameGrid();
    }

    private void InitializeGameGrid()
    {
        Dictionary<Coordinate, Piece?> board = _gameService.GetBoardDictionary();
        int maxRow = board.Keys.Max(r => r.row);
        int maxColumn = board.Keys.Max(r => r.column);
        int minRow = board.Keys.Min(r => r.row);
        int minColumn = board.Keys.Min(r => r.column);
        for (int i = minRow; i <= maxRow; i++)
        {
            GridBoard.RowDefinitions.Add(new RowDefinition());
        }

        for (int j = minColumn; j <= maxRow; j++)
        {
            GridBoard.ColumnDefinitions.Add(new ColumnDefinition());
        }

        int columnCount = GridBoard.ColumnDefinitions.Count;
        int rowCount = GridBoard.RowDefinitions.Count;
        for (int i = 0; i < columnCount; i ++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                int bottomBorder = j == rowCount -1 ? 1 : 0;
                int rightBorder = i == columnCount -1 ? 1 : 0;

                Thickness thickness = new Thickness(1, 1, bottomBorder, rightBorder);

                Border border = new Border
                {
                    BorderBrush = _brushBorder,
                    BorderThickness = thickness,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                };
                border.Child = new Avalonia.Controls.Shapes.Rectangle
                {
                    Fill = (i + j) % 2 == 0 ? _brushLightCells : _brushDarkCells,
                    
                };
                Grid.SetRow(border, i);
                Grid.SetColumn(border, j);
                GridBoard.Children.Add(border);
            }
        }

        foreach (KeyValuePair<Coordinate, Piece?> element in board.Where(b => b.Value != null))
        {
            Button button = new Button
            {
                Content = element.Value!.Type.ToString(),
                Background = element.Value!.Color == PieceColor.White ? _brushLightCells : _brushDarkCells,
                Foreground = element.Value!.Color == PieceColor.White ? _brushDarkCells : _brushLightCells,
                BorderBrush = element.Value!.Color == PieceColor.White ? _brushDarkCells : _brushLightCells,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            };
            button.Click += (sender, e) => PieceOnClick(element.Key);
            Coordinate GridPiecePosition = GridBoardCoordinateTranslator(GridBoard, element.Key);
            Grid.SetRow(button, GridPiecePosition.row);
            Grid.SetColumn(button, GridPiecePosition.column);
            _gridBoardPiecePosiotions.Add(element.Key, GridPiecePosition);
            GridBoard.Children.Add(button);
        }
    }

    private void PieceOnClick(Coordinate coordinate)
    {
        Coordinate coord = GridBoardCoordinateTranslator(GridBoard, coordinate);
    }

    /// <summary>
    /// Translate the coordinate from the grid to the board and opposite.
    /// In Board row number 0 is on the bottom, but in grid row number 0 is on the top.
    /// This is the reason there is need to use this translator.
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    private Coordinate GridBoardCoordinateTranslator(Grid grid, Coordinate coordinate)
    {
        int row = _flipBoard ? Math.Abs(grid.RowDefinitions.Count - coordinate.row - 1) : coordinate.row;
        int column = _flipBoard ? coordinate.column : Math.Abs(coordinate.column - grid.ColumnDefinitions.Count + 1);
        return new Coordinate(row, column);
    }
}
