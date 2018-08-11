using FlareHR.Battleship.ConsoleApp.Enums;
using FlareHR.Battleship.ConsoleApp.Models;

namespace FlareHR.Battleship.ConsoleApp.Services
{
    public interface IPlayerService
    {
        BoardEnum.PositionState[,] SetBoard(GameConfiguration gameConfiguration);
    }
}
