using FlareHR.Battleship.ConsoleApp.Enums;
using FlareHR.Battleship.ConsoleApp.Models;
using System;

namespace FlareHR.Battleship.ConsoleApp.Services
{
    public class PlayerService : IPlayerService, IDisposable
    {
        public BoardEnum.PositionState[,] SetBoard(GameConfiguration gameConfiguration)
        {
            var board = new BoardEnum.PositionState[gameConfiguration.Columns, gameConfiguration.Lines];

            Random rnd = new Random();
            for (int i = 0; i < gameConfiguration.Columns - 1; i++)
            {
                board[rnd.Next(gameConfiguration.Columns), rnd.Next(gameConfiguration.Lines)] = BoardEnum.PositionState.Floating;
            }

            return board;
        }

        public void Dispose()
        {
        }
    }

}
