using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;
using ChessGameLogic.Services.MoveStrategies;

namespace ChessGameLogic.Models.GameTypes;

public class GameStandard : GameType
{
    private Board _board;
    private List<(PieceColor color, bool isTurn)> _pieceColorTurn;
    private static readonly List<MoveDirection> s_movesDirectionWhitePawn = [MoveDirection.Up];
    private static readonly List<MoveDirection> s_movesDirectionWhitePawnEnPassant = [MoveDirection.UpLeft, MoveDirection.UpRight];
    private static readonly List<MoveDirection> s_movesDirectionBlackPawn = [MoveDirection.Down];
    private static readonly List<MoveDirection> s_movesDirectionBlackPawnEnPassant = [MoveDirection.DownLeft, MoveDirection.DownRight];
    private static readonly List<MoveDirection> s_movesDirectionRook = [MoveDirection.Up, MoveDirection.Down, MoveDirection.Left, MoveDirection.Right];
    private static readonly List<MoveDirection> s_movesDirectionBishop = [MoveDirection.UpLeft, MoveDirection.UpRight, MoveDirection.DownLeft, MoveDirection.DownRight];
    private static readonly List<MoveDirection> s_movesDirectionQueenAndKing = new(s_movesDirectionBishop.Concat(s_movesDirectionRook));
    private static readonly List<MoveDirection> s_movesDirectionKnight = new(s_movesDirectionBishop.Concat(new List<MoveDirection> { MoveDirection.LeftDown, MoveDirection.LeftUp, MoveDirection.RightDown, MoveDirection.RightUp }));
    private static readonly List<IMoveStrategy> s_moveStrategyWhitePawn = [new MoveInLine(s_movesDirectionWhitePawn, 2), new MoveEnPassant(s_movesDirectionWhitePawnEnPassant)];
    private static readonly List<IMoveStrategy> s_moveStrategyBlackPawn = [new MoveInLine(s_movesDirectionBlackPawn, 2), new MoveEnPassant(s_movesDirectionBlackPawnEnPassant)];
    private static readonly List<IMoveStrategy> s_moveStrategyRook = [new MoveInLine(s_movesDirectionRook)];
    private static readonly List<IMoveStrategy> s_moveStrategyBishop = [new MoveInLine(s_movesDirectionBishop)];
    private static readonly List<IMoveStrategy> s_moveStrategyQueen = [new MoveInLine(s_movesDirectionQueenAndKing)];
    private static readonly List<IMoveStrategy> s_moveStrategyKing = [new MoveInLine(s_movesDirectionQueenAndKing)];
    private static readonly List<IMoveStrategy> s_moveStrategyKnight = [new MoveKnightStyle(s_movesDirectionKnight)];

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
        Dictionary<Coordinate, Piece?> boardTab = [];

        for (int i = 0; i < 8; i++)
        {
            boardTab[new Coordinate(1, i)] = new Piece(PieceColor.White, PieceType.Pawn, s_moveStrategyWhitePawn);
        }

        boardTab[new Coordinate(0, 0)] = new Piece(PieceColor.White, PieceType.Rook, s_moveStrategyRook);
        boardTab[new Coordinate(0, 1)] = new Piece(PieceColor.White, PieceType.Knight, s_moveStrategyKnight);
        boardTab[new Coordinate(0, 2)] = new Piece(PieceColor.White, PieceType.Bishop, s_moveStrategyBishop);
        boardTab[new Coordinate(0, 3)] = new Piece(PieceColor.White, PieceType.Queen, s_moveStrategyQueen);
        boardTab[new Coordinate(0, 4)] = new Piece(PieceColor.White, PieceType.King, s_moveStrategyKing);
        boardTab[new Coordinate(0, 5)] = new Piece(PieceColor.White, PieceType.Bishop, s_moveStrategyBishop);
        boardTab[new Coordinate(0, 6)] = new Piece(PieceColor.White, PieceType.Knight, s_moveStrategyKnight);
        boardTab[new Coordinate(0, 7)] = new Piece(PieceColor.White, PieceType.Rook, s_moveStrategyRook);

        for (int i = 0; i < 8; i++)
        {
            boardTab[new Coordinate(6, i)] = new Piece(PieceColor.Black, PieceType.Pawn, s_moveStrategyBlackPawn);
        }

        boardTab[new Coordinate(7, 0)] = new Piece(PieceColor.Black, PieceType.Rook, s_moveStrategyRook);
        boardTab[new Coordinate(7, 1)] = new Piece(PieceColor.Black,PieceType.Knight, s_moveStrategyKnight);
        boardTab[new Coordinate(7, 2)] = new Piece(PieceColor.Black,PieceType.Bishop, s_moveStrategyBishop);
        boardTab[new Coordinate(7, 3)] = new Piece(PieceColor.Black,PieceType.Queen, s_moveStrategyQueen);
        boardTab[new Coordinate(7, 4)] = new Piece(PieceColor.Black,PieceType.King, s_moveStrategyKing);
        boardTab[new Coordinate(7, 5)] = new Piece(PieceColor.Black,PieceType.Bishop, s_moveStrategyBishop);
        boardTab[new Coordinate(7, 6)] = new Piece(PieceColor.Black,PieceType.Knight, s_moveStrategyKnight);
        boardTab[new Coordinate(7, 7)] = new Piece(PieceColor.Black,PieceType.Rook, s_moveStrategyRook);

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
