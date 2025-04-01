using ChessGameLogic.Enums;
using ChessGameLogic.Services.MoveStrategies;

namespace ChessGameLogic.Models.GameTypes;

public class GameStandard : GameType
{
    private Board _board;
    private List<(PieceColor color, bool isTurn)> _pieceColorTurn;

    public override Board Board => _board;
    public override List<(PieceColor color, bool isTurn)> PieceColorTurn => _pieceColorTurn;

    public GameStandard()
    {
        _board = GetStandardBoard();
        _pieceColorTurn = GetStandardPieceColorTurn();
    }

    public override bool MovePiece(Coordinate from, Coordinate to)
    {
        if (!_board.MovePiece(from, to))
        {
            return false;
        }
        return true;
    }

    public override bool ResetGame()
    {
        _board = GetStandardBoard();
        _pieceColorTurn = GetStandardPieceColorTurn();
        return true;
    }

    private static Board GetStandardBoard()
    {
        Dictionary<Coordinate, Piece?> boardTab = new();

        for (int i = 0; i < 8; i++)
        {
            boardTab[new Coordinate(1, i)] = new Piece(PieceColor.White, PieceType.Pawn, new PawnMoveStrategy(MoveDirection.Up));
        }

        boardTab[new Coordinate(0, 0)] = new Piece(PieceColor.White, PieceType.Rook, new RookMoveStrategy());
        boardTab[new Coordinate(0, 1)] = new Piece(PieceColor.White, PieceType.Knight, new KnightMoveStrategy());
        boardTab[new Coordinate(0, 2)] = new Piece(PieceColor.White, PieceType.Bishop, new BishopMoveStrategy());
        boardTab[new Coordinate(0, 3)] = new Piece(PieceColor.White, PieceType.Queen, new QueenMoveStrategy());
        boardTab[new Coordinate(0, 4)] = new Piece(PieceColor.White, PieceType.King, new KingMoveStrategy());
        boardTab[new Coordinate(0, 5)] = new Piece(PieceColor.White, PieceType.Bishop, new BishopMoveStrategy());
        boardTab[new Coordinate(0, 6)] = new Piece(PieceColor.White, PieceType.Knight, new KnightMoveStrategy());
        boardTab[new Coordinate(0, 7)] = new Piece(PieceColor.White, PieceType.Rook, new RookMoveStrategy());

        for (int i = 0; i < 8; i++)
        {
            boardTab[new Coordinate(6, i)] = new Piece(PieceColor.Black, PieceType.Pawn, new PawnMoveStrategy(MoveDirection.Down));
        }

        boardTab[new Coordinate(7, 0)] = new Piece(PieceColor.Black, PieceType.Rook, new RookMoveStrategy());
        boardTab[new Coordinate(7, 1)] = new Piece(PieceColor.Black, PieceType.Knight, new KnightMoveStrategy());
        boardTab[new Coordinate(7, 2)] = new Piece(PieceColor.Black, PieceType.Bishop, new BishopMoveStrategy());
        boardTab[new Coordinate(7, 3)] = new Piece(PieceColor.Black, PieceType.Queen, new QueenMoveStrategy());
        boardTab[new Coordinate(7, 4)] = new Piece(PieceColor.Black, PieceType.King, new KingMoveStrategy());
        boardTab[new Coordinate(7, 5)] = new Piece(PieceColor.Black, PieceType.Bishop, new BishopMoveStrategy());
        boardTab[new Coordinate(7, 6)] = new Piece(PieceColor.Black, PieceType.Knight, new KnightMoveStrategy());
        boardTab[new Coordinate(7, 7)] = new Piece(PieceColor.Black, PieceType.Rook, new RookMoveStrategy());

        for (int i = 2; i < 6; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                boardTab[new Coordinate(i, j)] = null;
            }
        }

        return new Board(boardTab);
    }

    private static List<(PieceColor color, bool isTurn)> GetStandardPieceColorTurn() =>
    [
            (PieceColor.White, true),
            (PieceColor.Black, false)
        ];
}
