using System.Collections.Generic;
using ChessGameLogic.Enums;
using ChessGameLogic.Models;

namespace ChessGameLogic.Utils
{
    public static class Rules
    {
        public static bool IsInCheck(Piece?[,] board, PieceColor playerColor)
        {
            Coordinate kingPosition = GetPieces(board, PieceType.King, playerColor)[0].position;
            List<(Piece piece, Coordinate position)> pieces = GetAllPieces(board);
            bool isInCheck = pieces.Where(pieces => pieces.piece.Color != playerColor)
                .Any(pieces => pieces.piece.MoveStrategy?.IsValidMove(board, pieces.position, kingPosition) ?? false);
            return isInCheck;
        }
        public static bool IsCheckMate(Piece?[,] board, PieceColor playerColor)
        {
            bool isInCheck = IsInCheck(board, playerColor);
            if (!isInCheck)
            {
                return false;
            }
            Piece king = GetPieces(board, PieceType.King, playerColor)[0].piece;
            if (!king.MoveStrategy.GetValidMoves(board, GetPieces(board, PieceType.King, playerColor)[0].position, out Coordinate[] validMoves))
            {
                return true;
            }

            if (validMoves.Length == 0)
            {
                return true;
            }

            return false;
        }
        public static bool IsDraw(Piece?[,] board, PieceColor playerColor)
        {
            List<(Piece piece, Coordinate position)> pieces = GetAllPieces(board);
            if (!pieces.Any(pieces => pieces.piece.Type != PieceType.King))
            {
                return true;
            }
            List<(int, int)[]> validMoves = [];
            foreach (var piece in pieces)
            {
                if (piece.piece.Color != playerColor)
                {
                    continue;
                }
                piece.piece.MoveStrategy.GetValidMoves(board, piece.position, out (int, int)[] moves);
                if (moves.Length > 0)
                {
                    validMoves.Add(moves);
                }
            }
            if (validMoves.Count() == 0)
            {
                return true;
            }

            return false;
        }

        public static List<(Piece piece, Coordinate position)> GetPieces(Piece?[,] board, PieceType pieceType, PieceColor color)
        {
            List<(Piece piece, Coordinate position)> pieces = [];

            var results = Enumerable.Range(0, board.GetLength(0))
                .SelectMany(row => Enumerable.Range(0, board.GetLength(1)), (row, column) => new { row, column, piece = board[row, column] })
                .Where(x => x.piece != null && x.piece.Type == pieceType && x.piece.Color == color)
                .Select(x => (x.piece!, (x.row, x.column)))
                .ToList();
            if (results != null && results.Count > 0)
                pieces.AddRange(results);

            return pieces;
        }

        public static List<(Piece piece, Coordinate position)> GetAllPieces(Piece?[,] board)
        {
            List<(Piece piece, Coordinate position)> pieces = [];

            var results = Enumerable.Range(0, board.GetLength(0))
                .SelectMany(row => Enumerable.Range(0, board.GetLength(1)), (row, column) => new { row, column, piece = board[row, column] })
                .Where(x => x.piece != null)
                .Select(x => (x.piece!, (x.row, x.column)))
                .ToList();
            if (results != null && results.Count > 0)
                pieces.AddRange(results);

            return pieces;
        }
    }
}
