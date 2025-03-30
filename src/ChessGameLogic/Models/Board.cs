namespace ChessGameLogic.Models
{
    public delegate void Notify();
    public class Board(Piece[,] boardTab)
    {
        public event Notify? PieceMoved;
        private Piece?[,] BoardTab { get; set; } = boardTab;

        public bool MovePiece((int row, int column) from, (int row, int column) to)
        {
            bool result;
            try
            {
                result = BoardTab[from.row, from.column]?.MoveStrategy?.MovePiece(BoardTab, from, to) ?? false;
                OnPieceMoved();
                return result;
            }
            catch (Exception ex) when (ex is IndexOutOfRangeException || ex is NullReferenceException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
                // TO DO: Log exception
            }
        }

        protected virtual void OnPieceMoved()
        {
            PieceMoved?.Invoke();
        }
    }
}
