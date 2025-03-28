namespace ChessGameLogic.Models
{
    internal class Board
    {
        public int [,] BoardTab { get; private set; }

        public Board()
        {
            BoardTab = new int[8, 8];
        }
    }
}
