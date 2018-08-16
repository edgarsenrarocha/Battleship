using FlareHR.Battleship.Entities;
using FlareHR.Battleship.Entities.Enums;
using System;

namespace FlareHR.Battleship.Services
{
    public class GameService : IGameService
    {
        private readonly IPlayerService _playerService;

        public GameService(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        /// <summary>
        /// Create the game
        /// </summary>
        /// <param name="dimension"></param>
        /// <param name="numberOfShips"></param>
        /// <returns></returns>
        public Game CreateGame(int dimension, int numberOfShips)
        {
            // Create Player One
            var playerOne = new Player
            {
                Board = _playerService.CreatePlayerBoard(dimension, numberOfShips),
                Number = PlayerNumber.One,
                NumberOfShipsLeft = numberOfShips,
                Color = ConsoleColor.Green
            };

            // Create Player Two
            var playerTwo = new Player
            {
                Board = _playerService.CreatePlayerBoard(dimension, numberOfShips),
                Number = PlayerNumber.Two,
                NumberOfShipsLeft = numberOfShips,
                Color = ConsoleColor.Red
            };

            var game = new Game
            {
                Columns = dimension,
                Lines = dimension,
                NumberOfShips = numberOfShips,
                PlayerOne = playerOne,
                PlayerTwo = playerTwo
            };

            return game;
        }

        /// <summary>
        /// Deploy the attack and validate entry
        /// </summary>
        /// <param name="gameConfiguration"></param>
        /// <param name="player"></param>
        /// <param name="columnInput"></param>
        /// <param name="lineInput"></param>
        /// <returns></returns>
        public Attack ValidateAttack(Game game, Player player, string columnInput, string lineInput)
        {
            var attack = new Attack();

            // Validate column entry
            if (!ValidateInput(columnInput, 1, game.Columns, out int validColumn))
            {
                attack.Valid = false;
                attack.Message = string.Format("Invalid column entry, minimum 1 maximum {0}:", game.Columns);

                // TODO: Add a custom service exception handler
                return attack;
            }
            else
            {
                attack.Column = validColumn;
            }

            // Validate Line entry
            if (!ValidateInput(lineInput, 1, game.Lines, out int validLine))
            {
                attack.Valid = false;
                attack.Message = string.Format("Invalid line entry, minimum 1 maximum {0}:", game.Lines);

                // TODO: Add a custom service exception handler
                return attack;
            }
            else
            {
                attack.Line = validLine;
            }

            // Set current board
            var positionAttacked = player.Board[attack.Column - 1, attack.Line - 1];

            // Validate if the position attacked has been attacked previously
            if (positionAttacked == PositionState.Attacked ||
                positionAttacked == PositionState.Sunk)
            {
                attack.Valid = false;
                attack.Message = string.Format("Position with column {0} and line {1} already attacked!", attack.Column, attack.Line);
                return attack;
            }

            // Valid if reached this point
            attack.Valid = true;

            return attack;
        }

        #region helpers
        /// <summary>
        /// Validate Input as an int between the bounds
        /// </summary>
        /// <param name="input"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ValidateInput(string input, int minValue, int maxValue, out int value)
        {
            if (int.TryParse(input, out value))
            {
                return value >= minValue && value <= maxValue;
            }

            return false;
        }
        #endregion
    }
}
