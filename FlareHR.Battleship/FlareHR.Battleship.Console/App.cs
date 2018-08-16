using FlareHR.Battleship.Entities;
using FlareHR.Battleship.Entities.Enums;
using FlareHR.Battleship.Services;
using System;

namespace FlareHR.Battleship.ConsoleApp
{
    public class App
    {
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;

        public App(IGameService gameService, IPlayerService playerService)
        {
            _gameService = gameService;
            _playerService = playerService;
        }

        public void Run()
        {
            var validInput = true;
            int dimension = 0;
            int shipsToBePlaced = 0;

            do
            {
                // Board dimension position Input
                Console.WriteLine("Input the Board dimension (minimum 1 and maximum {0}):", 50); // 50 is just to limit, can be any

                if (!_gameService.ValidateInput(Console.ReadLine(), 1, 50, out dimension))
                {
                    validInput = false;
                    Console.WriteLine("Invalid entry. (minimum 1 and maximum {0}):", 50);// 50 is just to limit, can be any
                    continue;
                }

                // Number of ships to be placed
                Console.WriteLine("Input number of Ships to be placed (minimum 1 and maximum {0}):", (dimension * dimension));// no more than number of positions
                if (!_gameService.ValidateInput(Console.ReadLine(), 1, (dimension * dimension), out shipsToBePlaced))
                {
                    validInput = false;
                    Console.WriteLine("Invalid entry. (minimum 1 and maximum {0}):", dimension);// no more than number of positions/dimension
                    continue;
                }

            } while (!validInput);

            // Create Game
            var game = _gameService.CreateGame(dimension, shipsToBePlaced);

            // Start the game
            StartGame(game);
        }

        public void StartGame(Game game)
        {
            // Player One first     
            Player currentPlayer = game.PlayerOne;

            // Keep asking the matrix until it reaches the maximum number of tries or all ships are found.
            do
            {
                Attack attack = null;

                // Change color depends on which player is playing 
                Console.ForegroundColor = currentPlayer.Color;

                Console.WriteLine("Player {0}\n", currentPlayer.Number);

                do
                {
                    // Column position Input
                    Console.WriteLine("Input column position number (minimum 1 and maximum {0}):", game.Columns);
                    var column = Console.ReadLine();

                    Console.WriteLine("Input line position number (minimum 1 and maximum {0}):", game.Lines);
                    var line = Console.ReadLine();

                    attack = _gameService.ValidateAttack(game, currentPlayer, column, line);
                    if (!attack.Valid)
                    {
                        Console.WriteLine(attack.Message);
                    }
                    else
                    {
                        // Set the position of the attack
                        currentPlayer.BoardLastState = currentPlayer.Board[attack.Column - 1, attack.Line - 1];
                    }

                    // Keep asking a valid entry
                } while (!attack.Valid);


                // Process and print the result of the attack to the player
                if (currentPlayer.BoardLastState == PositionState.Floating)
                {
                    // Subtract a ship when hit
                    currentPlayer.NumberOfShipsLeft--;

                    // Mark the spot where the ship was hit so it is not marked anymore
                    _playerService.ChangePlayerBoardState(currentPlayer, attack, PositionState.Sunk);

                    Console.ForegroundColor = ConsoleColor.Blue;// color of success
                    Console.WriteLine("\n------You've hit it!------\n------There are {0} ships left------\n", currentPlayer.NumberOfShipsLeft);
                }
                else
                {
                    // Mark the spot where the ship was hit so it is not marked again
                    _playerService.ChangePlayerBoardState(currentPlayer, attack, PositionState.Attacked);

                    Console.WriteLine("\n------You've missed------\n------You still have {0} ships left------\n", currentPlayer.NumberOfShipsLeft);
                }

                // Set who is the next player
                currentPlayer = currentPlayer.Number == PlayerNumber.One ? game.PlayerTwo : game.PlayerOne;

                // The game finishes either when you reach the maximum number ships sunk
            } while (currentPlayer.NumberOfShipsLeft > 0);


            // Game finished and show the result of the game
            Console.ForegroundColor = ConsoleColor.Blue;
            if (game.PlayerOne.NumberOfShipsLeft == game.PlayerTwo.NumberOfShipsLeft)
            {
                Console.WriteLine("-------------------------Game Tied!------------------------\n\n", currentPlayer.Number);
            }
            else
            {
                Console.WriteLine("-------------------------Well done Player {0}!------------------------\n\n", currentPlayer.Number);
            }
            Console.Read();
        }
    }
}
