using Generic;

namespace ChessGameLogic.Models
{
    public delegate void Notify();
    public class Board(Dictionary<Coordinate, Piece?> boardTab)
    {
        public event Notify? PieceMoved;
        public List<Move> Moves { get; set; } = new();
        public Dictionary<Coordinate, Piece?> BoardTab { get; set; } = boardTab;

        public bool MovePiece(Coordinate from, Coordinate to)
        {
            var move = default(Move);
            bool result = false;
            try
            {
                if (!BoardTab.TryGetValue(from, out Piece? piece) || piece == null)
                {
                    throw new NullReferenceException();
                }

                if (piece.IsCoordinateValidToMove(BoardTab, from, to))
                {
                    List<(Piece? piece, Coordinate position)>? takenPieces = new();
                    if (BoardTab.TryGetValue(to, out Piece? targetPiece) && targetPiece != null)
                    {
                        takenPieces.Add((targetPiece, to));
                    }
                    move = new(from, to, piece, takenPieces);
                    BoardTab[to] = piece;
                    BoardTab[from] = null;
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
                string pieceType = BoardTab.TryGetValue(from, out Piece? p) && p != null ? p.Type.ToString() : "Unknown";
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
