using FlareHR.Battleship.ConsoleApp.Enums;
using FlareHR.Battleship.ConsoleApp.Helpers;
using FlareHR.Battleship.ConsoleApp.Models;
using FlareHR.Battleship.ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FlareHR.Battleship.ConsoleApp
{
    class Program
    {

        public static void StartGame(Player playerOne, Player playerTwo, GameConfiguration gameConfiguration)
        {
            // Player One first     
            Player currentPlayer = playerOne;

            // Keep asking the matrix until it reaches the maximum number of tries or all ships are found.
            do
            {
                int columnNumberSelected = 0;
                int lineNumberSelected = 0;

                // Change color depends on which player is playing 
                Console.ForegroundColor = currentPlayer.Color;

                Console.WriteLine("Player {0}\n", currentPlayer.Number);
                BoardEnum.PositionState currentSelectionBoardState = new BoardEnum.PositionState();

                bool validEntries = true;

                do
                {
                    // Valid entry when staring 
                    validEntries = true;

                    // Column position Input
                    Console.WriteLine("Inform column position number (minimum 1 and maximum {0}):", gameConfiguration.Columns);
                    validEntries = Validator.ValidateInput(Console.ReadLine(), out columnNumberSelected);
                    if (!validEntries || columnNumberSelected > gameConfiguration.Columns)
                    {
                        Console.WriteLine("Invalid entry, minimum 1 maximum {0}:", gameConfiguration.Columns);
                        continue;
                    }

                    // Line position Input
                    Console.WriteLine("Inform line position number (minimum 1 and maximum {0}):", gameConfiguration.Columns);
                    validEntries = Validator.ValidateInput(Console.ReadLine(), out lineNumberSelected);
                    if (!validEntries || lineNumberSelected > gameConfiguration.Lines)
                    {
                        Console.WriteLine("Invalid entry, minimum 1 and maximum {0}:", gameConfiguration.Lines);
                        continue;
                    }

                    // Set current board
                    currentSelectionBoardState = currentPlayer.Board[columnNumberSelected - 1, lineNumberSelected - 1];

                    if (currentSelectionBoardState == BoardEnum.PositionState.Attacked || currentSelectionBoardState == BoardEnum.PositionState.Sunk)
                        Console.WriteLine("------Position already attacked------");

                } while (currentSelectionBoardState == BoardEnum.PositionState.Attacked || currentSelectionBoardState == BoardEnum.PositionState.Sunk || !validEntries);



                // Process and print the result of the attack to the player
                if (currentSelectionBoardState == BoardEnum.PositionState.Floating)
                {
                    // Subtract a ship when hit
                    currentPlayer.NumberOfShipsLeft--;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("------You've hit it!------\n------There are {0} ships left------", currentPlayer.NumberOfShipsLeft);

                    // Mark the spot where the ship was hit so it is not marked anymore
                    currentSelectionBoardState = BoardEnum.PositionState.Sunk;
                }
                else
                {
                    currentSelectionBoardState = BoardEnum.PositionState.Attacked;
                    Console.WriteLine("------You've missed------\n------You still have {0} ships left------", currentPlayer.NumberOfShipsLeft);
                }

                // Set last state of the board
                currentPlayer.Board[columnNumberSelected - 1, lineNumberSelected - 1] = currentSelectionBoardState;

                // Set who is the next player
                currentPlayer = currentPlayer.Number == PlayerEnum.Number.One ? playerTwo : playerOne;

                // The game finishes either when you reach the maximum number ships sunk
            } while (currentPlayer.NumberOfShipsLeft > 0);



            // Game finished and show the result of the game
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------------Well done Player {0}!---------------------------------\n\n", currentPlayer.Number);
            Console.Read();
        }

        static void Main(string[] args)
        {
            // DI for .net console apps
            var services = new ServiceCollection();
            services.AddTransient<PlayerService>();
            services.AddTransient<GameService>();
            var provider = services.BuildServiceProvider();

            // Ensure dispose call
            using (var playerService = provider.GetService<PlayerService>())
            {
                // Create game configuration
                var gameConfiguration = new GameConfiguration { Columns = 10, Lines = 10, NumberOfShips = 5 };

                // Create Player One
                var playerOne = new Player
                {
                    Board = playerService.SetBoard(gameConfiguration),
                    Number = PlayerEnum.Number.One,
                    NumberOfShipsLeft = gameConfiguration.NumberOfShips,
                    Color = ConsoleColor.Green
                };

                // Create layer Two
                var playerTwo = new Player
                {
                    Board = playerService.SetBoard(gameConfiguration),
                    Number = PlayerEnum.Number.Two,
                    NumberOfShipsLeft = gameConfiguration.NumberOfShips,
                    Color = ConsoleColor.Red
                };

                // Start the game
                StartGame(playerOne, playerTwo, gameConfiguration);
            }
        }
    }
}

