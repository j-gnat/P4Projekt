using Generic;

namespace ChessGameLogic.Models
{
    public delegate void Notify();
    public class Board(Piece[,] boardTab)
    {
        public event Notify? PieceMoved;
        public List<Move> Moves { get; set; } = [];
        public Piece?[,] BoardTab { get; set; } = boardTab;

        public bool MovePiece(Coordinate from, Coordinate to)
        {
            var move = default(Move);
            bool result = false;
            try
            {
                Piece piece = BoardTab[from.row, from.column] ?? throw new NullReferenceException();

                if (piece.MoveStrategy?.IsValidMove(BoardTab, from, to) ?? false)
                {
                    List<(Piece? piece, Coordinate position)>? takenPieces = [];
                    if (BoardTab[to.row, to.column] != null)
                    {
                        takenPieces.Add((BoardTab[to.row, to.column], to));
                    }
                    move = new (from, to, piece, takenPieces);
                    BoardTab[to.row, to.column] = piece;
                    BoardTab[from.row, from.column] = null;
                    OnPieceMoved();
                    result = true;
                }
                return result;
            }
            catch (Exception ex) when (ex is IndexOutOfRangeException || ex is NullReferenceException)
            {
                result = false;
                return result;
            }
            catch (Exception ex)
            {
                result = false;
                string pieceType = BoardTab[from.row, from.column]?.Type.ToString() ?? "Unknown";
                Logger.ErrorLog($"An unhandled error occurred while moving the piece \"{pieceType}\" from ({from.row}, {from.column}) to ({to.row}, {to.column}). {ex}");
                return result;
            }
            finally
            {
                if (result)
                {
                    Moves.Add(move);
                }
            }
        }

        protected virtual void OnPieceMoved()
        {
            PieceMoved?.Invoke();
        }
    }
}
