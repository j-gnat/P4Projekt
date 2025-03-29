using ChessGameLogic.Enums;
using ChessGameLogic.Interfaces;

namespace ChessGameLogic.Services.MoveStrategies
{
    internal class PawnMoveStrategy(MoveDirection moveDirection) : IMoveStrategy
    {
        MoveDirection _moveDirection = moveDirection;
    }
}
