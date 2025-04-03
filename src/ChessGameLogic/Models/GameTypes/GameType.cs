using ChessGameLogic.Enums;

namespace ChessGameLogic.Models.GameTypes;

public abstract class GameType
{
    protected Board _board { get; set; }
    public Board Board { get => _board; }
    public abstract List<(PieceColor color, bool isTurn)> PieceColorTurn { get; }

    protected GameType(Board board)
    {
        _board = board;
        Board.PieceMoved += ChangeTurn;
    }

    public abstract bool MovePiece(Coordinate from, Coordinate to);

    public abstract bool ResetGame();

    public PieceColor GetCurrentTurnColor()
    {
        return PieceColorTurn.Find(x => x.isTurn).color;
    }

    private void ChangeTurn()
    {
        int currentTurn = GetIndexCurrentPlayerTurn();
        int nextTurn = GetIndexNextPlayerTurn(currentTurn);
        PieceColorTurn[currentTurn] = (PieceColorTurn[currentTurn].color, false);
        PieceColorTurn[nextTurn] = (PieceColorTurn[nextTurn].color, true);
    }

    private int GetIndexNextPlayerTurn(int currentTurn)
    {
        return currentTurn == PieceColorTurn.Count - 1 ? 0 : currentTurn + 1;
    }

    private int GetIndexCurrentPlayerTurn()
    {
        return PieceColorTurn.IndexOf(PieceColorTurn.Find(x => x.isTurn));
    }
}
