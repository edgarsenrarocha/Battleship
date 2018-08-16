using FlareHR.Battleship.Entities;
using FlareHR.Battleship.Entities.Enums;

namespace FlareHR.Battleship.Services
{
    public interface IPlayerService
    {
        PositionState[,] CreatePlayerBoard(int dimension, int numberOfShips);
        void ChangePlayerBoardState(Player player, Attack attack, PositionState newBoardState);

    }
}
