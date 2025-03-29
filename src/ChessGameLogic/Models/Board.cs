namespace ChessGameLogic.Models
{
    public class Board(Piece[,] boardTab)
    {
        public Piece [,] BoardTab { get; set; } = boardTab;        
    }
}
