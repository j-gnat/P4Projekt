using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using ChessGameLogic.Enums;
using ChessGameLogic.Models;
using ChessGameLogic.Services;
using ChessGameUI.Models;

namespace ChessGameUI.Views;

public partial class MainWindow : Window
{
    private readonly ChessGame _gameService;
    private readonly Grid _gridBoard;
    private readonly BoardGridTranslator _boardGridTranslator;
    private readonly IBrush _brushLightCells = Brushes.Peru;
    private readonly IBrush _brushDarkCells = Brushes.SaddleBrown;
    private readonly IBrush _brushBorder = Brushes.Black;
    private readonly List<Button> _moveButtons = [];
    private double _currentCellSize = 42;

    public MainWindow()
    {
        InitializeComponent();
        _gameService = new ChessGame();
        _boardGridTranslator = new BoardGridTranslator(_gameService.GetBoard());
        _gridBoard = new Grid();
        CanvasBoard.Children.Add(_gridBoard);
        Loaded += (s, e) => InitializeGameGrid();
        SizeChanged += (s, e) => ResizeGameGrid();
        ResetButton.Click += (s, e) => ResetGame();
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
        _currentCellSize = Math.Min(width, Height) / Math.Max(_gridBoard.ColumnDefinitions.Count, _gridBoard.RowDefinitions.Count);

        GridLength cellDim = new(_currentCellSize);
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
            var piece = element.Value!;
            Button button = new()
            {
                Content = GetPieceSymbol(piece.Type, piece.Color),
                FontSize = _currentCellSize * 0.8,
                Foreground = piece.Color == PieceColor.White ? Brushes.Cornsilk : Brushes.Black,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
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

    private void ResetGame()
    {
        _gameService.ResetGame();
        InitializeGameGrid();
    }
    private static string GetPieceSymbol(PieceType type, PieceColor color)
    {
        return (type, color) switch
        {
            (PieceType.King, PieceColor.White) => "♔",
            (PieceType.Queen, PieceColor.White) => "♕",
            (PieceType.Rook, PieceColor.White) => "♖",
            (PieceType.Bishop, PieceColor.White) => "♗",
            (PieceType.Knight, PieceColor.White) => "♘",
            (PieceType.Pawn, PieceColor.White) => "♙",
            (PieceType.King, PieceColor.Black) => "♚",
            (PieceType.Queen, PieceColor.Black) => "♛",
            (PieceType.Rook, PieceColor.Black) => "♜",
            (PieceType.Bishop, PieceColor.Black) => "♝",
            (PieceType.Knight, PieceColor.Black) => "♞",
            (PieceType.Pawn, PieceColor.Black) => "♙",
            _ => ""
        };
    }
}
