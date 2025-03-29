namespace ChessGameLogic.Models
{
    public delegate void Notify();
    public class Board(Piece[,] boardTab)
    {
        public event Notify? PieceMoved;
        private Piece?[,] BoardTab { get; set; } = boardTab;

        public bool MovePiece((int x, int y) from, (int x, int y) to)
        {
            try
            {
                BoardTab[to.x, to.y] = BoardTab[from.x, from.y];
                BoardTab[from.x, from.y] = null;
                OnPieceMoved();
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        protected virtual void OnPieceMoved()
        {
            PieceMoved?.Invoke();
        }
    }
}
