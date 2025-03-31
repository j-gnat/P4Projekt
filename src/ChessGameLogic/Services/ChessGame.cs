using System.Security.AccessControl;
using ChessGameLogic.Enums;
using ChessGameLogic.Models;
using ChessGameLogic.Models.GameTypes;
using ChessGameLogic.Services.MoveStrategies;

namespace ChessGameLogic.Services
{
    public class ChessGame
    {
        private GameType _gameType;

        public ChessGame()
        {
            _gameType = new GameStandard();
        }

        public ChessGame(GameType gameType)
        {
            _gameType = gameType;
        }

        public bool ChangeGameType(GameType gameType)
        {
            _gameType = gameType;
            return true;
        }

        public bool MakeMove(Coordinate from, Coordinate to)
        {
            return _gameType.Board.MovePiece(from, to);
        }

        public bool ResetGame()
        {
            return _gameType.ResetGame();
        }

        public bool UndoMove()
        {
            throw new System.NotImplementedException();
        }

        public bool SaveGame(string path)
        {
            throw new System.NotImplementedException();
        }

        public bool LoadGame(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}
