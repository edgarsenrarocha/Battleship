using FlareHR.Battleship.Entities;
using FlareHR.Battleship.Entities.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace FlareHR.Battleship.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ILogger<IPlayerService> _logger;

        public PlayerService(ILogger<IPlayerService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Create the board to the player
        /// </summary>
        /// <param name="dimension"></param>
        /// <param name="numberOfShips"></param>
        /// <returns></returns>
        public PositionState[,] CreatePlayerBoard(int dimension, int numberOfShips)
        {
            var board = new PositionState[dimension, dimension];

            for (int i = 0; i < numberOfShips; i++)
            {
                var position = new Position();

                // Make sure that a ship is not placed in the position twice
                do
                {
                    position = GeneratePosition(dimension, dimension);

                } while (board[position.Column, position.Line] == PositionState.Floating);

                board[position.Column, position.Line] = PositionState.Floating;
            }

            return board;
        }

        /// <summary>
        /// Change board state
        /// </summary>
        /// <param name="player"></param>
        /// <param name="attack"></param>
        /// <param name="newBoardState"></param>
        public void ChangePlayerBoardState(Player player, Attack attack, PositionState newBoardState)
        {
            var oldState = player.BoardLastState;
            player.Board[attack.Column - 1, attack.Line - 1] = newBoardState;

            // Log the board stater tracker
            _logger.LogWarning(string.Format("The player {0}'s board at position column {1} and line {2} was {3} and now is {4}",
                player.Number,
                attack.Column,
                attack.Line,
                oldState.ToString(),
                newBoardState.ToString()));

            // Due to the limitations with Microsoft Ilogger not having writing the text in a correct sequence on the screen.
            Thread.Sleep(2);
        }

        /// <summary>
        /// Generate the position to place the ship
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="lines"></param>
        /// <returns></returns>
        private Position GeneratePosition(int columns, int lines)
        {
            var position = new Position();
            Random randon = new Random();

            position.Column = randon.Next(columns);
            position.Line = randon.Next(lines);

            return position;
        }
    }
}
