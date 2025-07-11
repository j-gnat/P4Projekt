﻿using ChessGameLogic.Models;
using ChessGameLogic.Models.GameTypes;

namespace ChessGameLogic.Services;

public class ChessGame
{
    private GameType _gameType;

    public event Notify? CheckMate;

    public ChessGame()
    {
        _gameType = new GameStandard();
    }

    public ChessGame(GameType gameType)
    {
        _gameType = gameType;
    }

    public bool ChangeGameType(GameType gameType)
    {
        _gameType = gameType;
        return true;
    }

    public bool MakeMove(Coordinate from, Coordinate to)
    {
        var result = _gameType.MovePiece(from, to);
        if ( _gameType.IsCheckMate)
        {
            OnCheckMate();
        }
        return result;
    }

    public IEnumerable<Coordinate> GetValidMoves(Coordinate from)
    {
        return _gameType.GetValidMoves(from);
    }

    public bool ResetGame()
    {
        return _gameType.ResetGame();
    }

    public bool UndoMove()
    {
        throw new System.NotImplementedException();
    }

    public bool SaveGame(string path)
    {
        throw new System.NotImplementedException();
    }

    public bool LoadGame(string path)
    {
        throw new System.NotImplementedException();
    }
    public Dictionary<Coordinate, Piece?> GetBoardDictionary()
    {
        return _gameType.Board.BoardTab;
    }

    public Board GetBoard()
    {
        return _gameType.Board;
    }

    private void OnCheckMate()
    {
        CheckMate?.Invoke();
    }
}
