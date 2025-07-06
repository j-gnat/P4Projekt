// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChessGameLogic.Models.Rules;
using ChessGameLogic.Models;
using ChessGameLogic.Models.GameTypes;

namespace ChessGameLogicTest;

public class Tests
{
    private readonly GameStandard _game = new ();
    private readonly CheckRule _check = new ();

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void Test1()
    {
        _game.MovePiece(new Coordinate(1, 4), new Coordinate(3,4));
        _game.MovePiece(new Coordinate(6,4), new Coordinate(4,4));
        _game.MovePiece(new Coordinate(1,0), new Coordinate(2,0));
        _game.MovePiece(new Coordinate(7,5), new Coordinate(3,1));
        var test = _game.GetValidMoves(new Coordinate(2,0));

        var moves = new List<Coordinate>();

        foreach (var move in test)
        {
            if (_check.IsValidMove(new Coordinate(2, 0), move, _game.Board.BoardTab))
            {
                moves.Add(move);
            }
        }

        Assert.Multiple(() =>
        {
            Assert.True(moves.Count() == 1);
            Assert.True(moves[0].column == 1 && moves[0].row == 3);
        });
        
    }
}
