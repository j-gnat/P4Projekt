using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;
using ChessGameLogic.Services.MoveStrategies;

namespace ChessGameLogic.Models.GameTypes;

public class GameStandard : GameType
{
    private List<(PieceColor color, bool isTurn)> _pieceColorTurn;
    private static readonly List<MoveDirection> s_movesDirectionWhitePawn = [MoveDirection.Up];
    private static readonly List<MoveDirection> s_movesDirectionWhitePawnEnPassant = [MoveDirection.DownLeft, MoveDirection.DownRight];
    private static readonly List<MoveDirection> s_movesDirectionBlackPawn = [MoveDirection.Down];
    private static readonly List<MoveDirection> s_movesDirectionBlackPawnEnPassant = [MoveDirection.UpLeft, MoveDirection.UpRight];
    private static readonly List<MoveDirection> s_movesDirectionRook = [MoveDirection.Up, MoveDirection.Down, MoveDirection.Left, MoveDirection.Right];
    private static readonly List<MoveDirection> s_movesDirectionBishop = [MoveDirection.UpLeft, MoveDirection.UpRight, MoveDirection.DownLeft, MoveDirection.DownRight];
    private static readonly List<MoveDirection> s_movesDirectionQueenAndKing = [.. s_movesDirectionBishop.Concat(s_movesDirectionRook)];
    private static readonly List<MoveDirection> s_movesDirectionKnight = [.. s_movesDirectionBishop.Concat(new List<MoveDirection> { MoveDirection.LeftDown, MoveDirection.LeftUp, MoveDirection.RightDown, MoveDirection.RightUp })];
    private static readonly List<IMoveStrategy> s_moveStrategyWhitePawn = [new MoveInLine(s_movesDirectionWhitePawn, 2), new MoveInLine(s_movesDirectionWhitePawnEnPassant, 1)];
    private static readonly List<IMoveStrategy> s_moveStrategyBlackPawn = [new MoveInLine(s_movesDirectionBlackPawn, 2), new MoveInLine(s_movesDirectionBlackPawnEnPassant, 1)];
    private static readonly List<IMoveStrategy> s_moveStrategyRook = [new MoveInLine(s_movesDirectionRook)];
    private static readonly List<IMoveStrategy> s_moveStrategyBishop = [new MoveInLine(s_movesDirectionBishop)];
    private static readonly List<IMoveStrategy> s_moveStrategyQueen = [new MoveInLine(s_movesDirectionQueenAndKing)];
    private static readonly List<IMoveStrategy> s_moveStrategyKing = [new MoveInLine(s_movesDirectionQueenAndKing, 1)];
    private static readonly List<IMoveStrategy> s_moveStrategyKnight = [new MoveKnightStyle(s_movesDirectionKnight)];

    public override List<(PieceColor color, bool isTurn)> PieceColorTurn => _pieceColorTurn;

    public GameStandard() : base(GetStandardBoard())
    {
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

    public override IEnumerable<Coordinate> GetValidMoves(Coordinate from)
    {
        Piece? piece = _board.GetPiece(from);
        return piece == null
            ? throw new InvalidOperationException("There is no piece on the given position.")
            : piece.GetValidMoves(_board.BoardTab, from);
    }

    private static Board GetStandardBoard()
    {
        Dictionary<Coordinate, Piece?> boardTab = [];

        for (int column = 0; column < 8; column++)
        {
            boardTab[new Coordinate(1, column)] = GetNewPiece(PieceColor.White, PieceType.Pawn, s_moveStrategyWhitePawn);
        }

        boardTab[new Coordinate(0, 0)] = GetNewPiece(PieceColor.White, PieceType.Rook, s_moveStrategyRook);
        boardTab[new Coordinate(0, 1)] = GetNewPiece(PieceColor.White, PieceType.Knight, s_moveStrategyKnight);
        boardTab[new Coordinate(0, 2)] = GetNewPiece(PieceColor.White, PieceType.Bishop, s_moveStrategyBishop);
        boardTab[new Coordinate(0, 3)] = GetNewPiece(PieceColor.White, PieceType.Queen, s_moveStrategyQueen);
        boardTab[new Coordinate(0, 4)] = GetNewPiece(PieceColor.White, PieceType.King, s_moveStrategyKing);
        boardTab[new Coordinate(0, 5)] = GetNewPiece(PieceColor.White, PieceType.Bishop, s_moveStrategyBishop);
        boardTab[new Coordinate(0, 6)] = GetNewPiece(PieceColor.White, PieceType.Knight, s_moveStrategyKnight);
        boardTab[new Coordinate(0, 7)] = GetNewPiece(PieceColor.White, PieceType.Rook, s_moveStrategyRook);

        for (int column = 0; column < 8; column++)
        {
            boardTab[new Coordinate(6, column)] = GetNewPiece(PieceColor.Black, PieceType.Pawn, s_moveStrategyBlackPawn);
        }

        boardTab[new Coordinate(7, 0)] = GetNewPiece(PieceColor.Black, PieceType.Rook, s_moveStrategyRook);
        boardTab[new Coordinate(7, 1)] = GetNewPiece(PieceColor.Black,PieceType.Knight, s_moveStrategyKnight);
        boardTab[new Coordinate(7, 2)] = GetNewPiece(PieceColor.Black,PieceType.Bishop, s_moveStrategyBishop);
        boardTab[new Coordinate(7, 3)] = GetNewPiece(PieceColor.Black,PieceType.Queen, s_moveStrategyQueen);
        boardTab[new Coordinate(7, 4)] = GetNewPiece(PieceColor.Black,PieceType.King, s_moveStrategyKing);
        boardTab[new Coordinate(7, 5)] = GetNewPiece(PieceColor.Black,PieceType.Bishop, s_moveStrategyBishop);
        boardTab[new Coordinate(7, 6)] = GetNewPiece(PieceColor.Black,PieceType.Knight, s_moveStrategyKnight);
        boardTab[new Coordinate(7, 7)] = GetNewPiece(PieceColor.Black,PieceType.Rook, s_moveStrategyRook);

        for (int row = 2; row < 6; row++)
        {
            for (int column = 0; column < 8; column++)
            {
                boardTab[new Coordinate(row, column)] = null;
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
