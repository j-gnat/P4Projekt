﻿using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;

namespace ChessGameLogic.Models.GameTypes;

public abstract class GameType
{
    protected Board _board { get; set; }
    public Board Board { get => _board; }
    public abstract List<(PieceColor color, bool isTurn)> PieceColorTurn { get; }

    public bool IsCheckMate = false;

    protected GameType(Board board)
    {
        _board = board;
        Board.PieceMoved += ChangeTurn;
    }

    public abstract bool MovePiece(Coordinate from, Coordinate to);

    public abstract bool ResetGame();

    public abstract IEnumerable<Coordinate> GetValidMoves(Coordinate from);

    public PieceColor GetCurrentTurnColor()
    {
        return PieceColorTurn.Find(x => x.isTurn).color;
    }

    private int GetIndexNextPlayerTurn(int currentTurn)
    {
        return currentTurn == PieceColorTurn.Count - 1 ? 0 : currentTurn + 1;
    }

    private int GetIndexCurrentPlayerTurn()
    {
        return PieceColorTurn.IndexOf(PieceColorTurn.Find(x => x.isTurn));
    }
    protected void ChangeTurn()
    {
        int currentTurn = GetIndexCurrentPlayerTurn();
        int nextTurn = GetIndexNextPlayerTurn(currentTurn);
        PieceColorTurn[currentTurn] = (PieceColorTurn[currentTurn].color, false);
        PieceColorTurn[nextTurn] = (PieceColorTurn[nextTurn].color, true);
    }

    protected static Piece GetNewPiece(PieceColor color, PieceType type, List<IMoveStrategy> moveStrategy) =>
        new()
        {
            Color = color,
            Type = type,
            MoveStrategy = moveStrategy
        };

    protected bool IsPieceTurn(Coordinate from) =>
        PieceColorTurn.Where(p => p.isTurn == true)
            .Any(p => p.color == Board.GetPiece(from)?.Color);
}
