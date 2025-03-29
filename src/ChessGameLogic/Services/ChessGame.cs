using System.Security.AccessControl;
using ChessGameLogic.Enums;
using ChessGameLogic.Models;
using ChessGameLogic.Services.MoveStrategies;

namespace ChessGameLogic.Services
{
    public class ChessGame
    {
        private GameType _gameType = GameType.Standard;
        private Board _board;
        private List<(PieceColor color, bool isTurn)> _pieceColorTurn;

        public ChessGame()
        {
            _gameType = GameType.Standard;
            (Board board, List<(PieceColor color, bool isTurn)> pieceColorTurn) gameObjects = GetGameObjects(_gameType);
            _board = gameObjects.board;
            _pieceColorTurn = gameObjects.pieceColorTurn;
        }

        public ChessGame(GameType gameType)
        {
            _gameType = gameType;
            (Board board, List<(PieceColor color, bool isTurn)> pieceColorTurn) gameObjects = GetGameObjects(_gameType);
            _board = gameObjects.board;
            _pieceColorTurn = gameObjects.pieceColorTurn;
        }

        public bool MakeMove((int x, int y) from, (int x, int y) to)
        {
            return true;
        }

        public void ResetGame()
        {
            (Board board, List<(PieceColor color, bool isTurn)> pieceColorTurn) gameObjects = GetGameObjects(_gameType);
            _board = gameObjects.board;
            _pieceColorTurn = gameObjects.pieceColorTurn;
        }

        private (Board board, List<(PieceColor color, bool isTurn)> pieceColorTurn) GetGameObjects(GameType gameType)
        {
            Board board;
            List<(PieceColor color, bool isTurn)> pieceColorTurn;
            switch (gameType)
            {
                case GameType.Standard:
                    board = GetStandardBoard();
                    pieceColorTurn = GetStandardPieceColorTurn();
                    break;
                default:
                    board = GetStandardBoard();
                    pieceColorTurn = GetStandardPieceColorTurn();
                    break;
            }
            return (board, pieceColorTurn);
        }

        private Board GetStandardBoard()
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

        private List<(PieceColor color, bool isTurn)> GetStandardPieceColorTurn()
        {
            return new List<(PieceColor color, bool isTurn)>
            {
                (PieceColor.White, true),
                (PieceColor.Black, false)
            };
        }
    }
}
