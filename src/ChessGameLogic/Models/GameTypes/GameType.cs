using ChessGameLogic.Enums;

namespace ChessGameLogic.Models.GameTypes
{
    public abstract class GameType
    {
        public abstract Board Board { get; }
        public abstract List<(PieceColor color, bool isTurn)> PieceColorTurn { get; }

        protected GameType()
        {
            Board.PieceMoved += ChangeTurn;
        }

        public abstract bool ResetGame();

        public PieceColor GetCurrentTurnColor()
        {
            return PieceColorTurn.Find(x => x.isTurn).color;
        }

        private void ChangeTurn()
        {
            int currentTurn = PieceColorTurn.IndexOf(PieceColorTurn.Find(x => x.isTurn));
            int nextTurn = currentTurn == PieceColorTurn.Count - 1 ? 0 : currentTurn + 1;
            PieceColorTurn[currentTurn] = (PieceColorTurn[currentTurn].color, false);
            PieceColorTurn[nextTurn] = (PieceColorTurn[nextTurn].color, true);
        }
    }
}
