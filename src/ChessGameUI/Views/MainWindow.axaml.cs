using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using ChessGameLogic.Models;
using ChessGameLogic.Services;
using ChessGameLogic.Enums;
using Avalonia.Media;
using Avalonia;
using System;
using ChessGameUI.Models;

namespace ChessGameUI.Views;

public partial class MainWindow : Window
{
    private readonly ChessGame _gameService;
    private readonly Grid _gridBoard;
    private readonly BoardGridTranslator _boardGridTranslator;
    private readonly IBrush _brushLightCells = Brushes.BlanchedAlmond;
    private readonly IBrush _brushDarkCells = Brushes.SaddleBrown;
    private readonly IBrush _brushLightPieces = Brushes.White;
    private readonly IBrush _brushDarkPieces = Brushes.Black;
    private readonly IBrush _brushBorder = Brushes.Black;
    private readonly List<Button> _moveButtons = [];

    public MainWindow()
    {
        InitializeComponent();
        _gameService = new ChessGame();
        _boardGridTranslator = new BoardGridTranslator(_gameService.GetBoard());
        _gridBoard = new Grid();
        CanvasBoard.Children.Add( _gridBoard );
        Loaded += (s, e) => InitializeGameGrid();
        SizeChanged += (s, e) => ResizeGameGrid();
    }

    private void InitializeGameGrid()
    {
        InitializeBoardUI();
        PutPiecesOnTheBoard();
    }

    private void InitializeBoardUI()
    {
        AddRowsAndColumns();
        SetVisualPropertiesForEachCell();
        ResizeGameGrid();
    }

    private void AddRowsAndColumns()
    {
        _gridBoard.RowDefinitions.Clear();
        _gridBoard.ColumnDefinitions.Clear();
        for (int row = 0; row <= _boardGridTranslator.RowsCount; row++)
        {
            _gridBoard.RowDefinitions.Add(new RowDefinition());
        }

        for (int column = 0; column <= _boardGridTranslator.ColumnsCount; column++)
        {
            _gridBoard.ColumnDefinitions.Add(new ColumnDefinition());
        }
    }

    private void SetVisualPropertiesForEachCell()
    {
        for (int row = 0; row <= _boardGridTranslator.ColumnsCount; row++)
        {
            for (int column = 0; column <= _boardGridTranslator.ColumnsCount; column++)
            {
                int bottomBorder = row == _boardGridTranslator.RowsCount ? 1 : 0;
                int rightBorder = column == _boardGridTranslator.ColumnsCount ? 1 : 0;

                Thickness thickness = new(1, 1, rightBorder, bottomBorder); // <object property=" left,top,right,bottom" ... />  
                Border border = new()
                {
                    Background = (row + column) % 2 == 0 ? _brushLightCells : _brushDarkCells,
                    BorderBrush = _brushBorder,
                    BorderThickness = thickness,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                };
                Grid.SetRow(border, row);
                Grid.SetColumn(border, column);
                _gridBoard.Children.Add(border);
            }
        }
    }

    private void ResizeGameGrid()
    {
        if (_gridBoard.Children.Count == 0)
            return;

        double width = GridUI.ColumnDefinitions[1].ActualWidth;
        GridLength cellDim = new(Math.Min(width, Height) / Math.Max(_gridBoard.ColumnDefinitions.Count, _gridBoard.RowDefinitions.Count));
        foreach (RowDefinition row in _gridBoard.RowDefinitions)
        {
            row.Height = cellDim;
        }
        foreach (ColumnDefinition column in _gridBoard.ColumnDefinitions)
        {
            column.Width = cellDim;
        }
    }

    private void PutPiecesOnTheBoard()
    {
        Dictionary<Coordinate, Piece?> board = _gameService.GetBoardDictionary();
        foreach (KeyValuePair<Coordinate, Piece?> element in board.Where(b => b.Value != null))
        {
            Button button = new()
            {
                Content = element.Value!.Type.ToString(),
                Background = element.Value!.Color == PieceColor.White ? _brushLightPieces : _brushDarkPieces,
                Foreground = element.Value!.Color == PieceColor.White ? _brushDarkPieces : _brushLightPieces,
                BorderBrush = element.Value!.Color == PieceColor.White ? _brushDarkPieces : _brushLightPieces,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            };
            button.Click += (sender, e) => PieceOnClick(element.Key);
            Coordinate GridPiecePosition = _boardGridTranslator.TranslateCoordinte(_gridBoard, element.Key);
            Grid.SetRow(button, GridPiecePosition.row);
            Grid.SetColumn(button, GridPiecePosition.column);
            _gridBoard.Children.Add(button);
        }
    }

    private void PieceOnClick(Coordinate coordinate)
    {
        foreach (Button button in _moveButtons)
        {
            _gridBoard.Children.Remove(button);
        }
        List<Coordinate> moves = [.. _gameService.GetValidMoves(coordinate)];
        Brush brush = new RadialGradientBrush()
        {
            GradientStops =
            [
                new GradientStop(Colors.Black, 0),
                new GradientStop(Colors.Transparent, 1)
            ],
        };
        foreach (Coordinate move in moves)
        {
            Button moveButton = new()
            {
                Background = brush,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
            };

            moveButton.Click += (sender, e) => MoveOnClick(coordinate, move);
            Coordinate translatedMove = _boardGridTranslator.TranslateCoordinte(_gridBoard, move);
            Grid.SetRow(moveButton, translatedMove.row);
            Grid.SetColumn(moveButton, translatedMove.column);
            _moveButtons.Add(moveButton);
            _gridBoard.Children.Add(moveButton);
        }
    }

    private void MoveOnClick(Coordinate from, Coordinate to)
    {
        _gameService.MakeMove(from, to);
        InitializeGameGrid();
    }
}
