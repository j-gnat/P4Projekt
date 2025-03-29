using ChessGameLogic.Enums;
using ChessGameLogic.Services.MoveStrategies;

namespace ChessGameLogic.Models.GameTypes
{
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

        public override bool ResetGame()
        {
            _board = GetStandardBoard();
            _pieceColorTurn = GetStandardPieceColorTurn();
            return true;
        }

        private static Board GetStandardBoard()
        {
            Piece[,] boardTab = new Piece[8, 8];

            boardTab[0, 0] = new Piece(PieceColor.White, PieceType.Rook, new RookMoveStrategy());
            boardTab[0, 1] = new Piece(PieceColor.White, PieceType.Knight, new KnightMoveStrategy());
            boardTab[0, 2] = new Piece(PieceColor.White, PieceType.Bishop, new BishopMoveStrategy());
            boardTab[0, 3] = new Piece(PieceColor.White, PieceType.Queen, new QueenMoveStrategy());
            boardTab[0, 4] = new Piece(PieceColor.White, PieceType.King, new KingMoveStrategy());
            boardTab[0, 5] = new Piece(PieceColor.White, PieceType.Bishop, new BishopMoveStrategy());
            boardTab[0, 6] = new Piece(PieceColor.White, PieceType.Knight, new KnightMoveStrategy());
            boardTab[0, 7] = new Piece(PieceColor.White, PieceType.Rook, new RookMoveStrategy());
            for (int i = 0; i < 8; i++)
            {
                boardTab[1, i] = new Piece(PieceColor.White, PieceType.Pawn, new PawnMoveStrategy(MoveDirection.Up));
            }

            boardTab[7, 0] = new Piece(PieceColor.Black, PieceType.Rook, new RookMoveStrategy());
            boardTab[7, 1] = new Piece(PieceColor.Black, PieceType.Knight, new KnightMoveStrategy());
            boardTab[7, 2] = new Piece(PieceColor.Black, PieceType.Bishop, new BishopMoveStrategy());
            boardTab[7, 3] = new Piece(PieceColor.Black, PieceType.Queen, new QueenMoveStrategy());
            boardTab[7, 4] = new Piece(PieceColor.Black, PieceType.King, new KingMoveStrategy());
            boardTab[7, 5] = new Piece(PieceColor.Black, PieceType.Bishop, new BishopMoveStrategy());
            boardTab[7, 6] = new Piece(PieceColor.Black, PieceType.Knight, new KnightMoveStrategy());
            boardTab[7, 7] = new Piece(PieceColor.Black, PieceType.Rook, new RookMoveStrategy());
            for (int i = 0; i < 8; i++)
            {
                boardTab[6, i] = new Piece(PieceColor.Black, PieceType.Pawn, new PawnMoveStrategy(MoveDirection.Down));
            }

            return new Board(boardTab);
        }

        private static List<(PieceColor color, bool isTurn)> GetStandardPieceColorTurn() =>
        [
                (PieceColor.White, true),
                (PieceColor.Black, false)
            ];
    }
}
