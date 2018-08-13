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

            Random randon = new Random();
            for (int i = 0; i < gameConfiguration.NumberOfShips; i++)
            {
                int columnNum;
                int lineNum;

                do
                {
                    columnNum = randon.Next(gameConfiguration.Columns);
                    lineNum = randon.Next(gameConfiguration.Lines);

                } while (board[columnNum, lineNum] == BoardEnum.PositionState.Floating);

                board[columnNum, lineNum] = BoardEnum.PositionState.Floating;
            }

            return board;
        }

        public void Dispose()
        {
        }
    }

}
