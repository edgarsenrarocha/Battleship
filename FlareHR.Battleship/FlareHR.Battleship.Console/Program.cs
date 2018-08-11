using FlareHR.Battleship.ConsoleApp.Enums;
using FlareHR.Battleship.ConsoleApp.Models;
using System;

namespace FlareHR.Battleship.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int matrixDimension = 10;
            int columnNumberSelected;
            int lineNumberSelected;

            var playerOne = new Player
            {
                Board = new  BoardEnum.PositionState[matrixDimension, matrixDimension],
                Number = PlayerEnum.Number.One,
                NumberOfShipsLeft = matrixDimension / 2,
                Color = ConsoleColor.Green
            };

            var playerTwo = new Player
            {
                Board = new BoardEnum.PositionState[matrixDimension, matrixDimension],
                Number = PlayerEnum.Number.Two,
                NumberOfShipsLeft = matrixDimension / 2,
                Color = ConsoleColor.Red
            };

            Player currentPlayer = playerOne;// Player One first


            // Spread the ships around the matrix
            Random rnd = new Random();
            for (int i = 0; i < matrixDimension - 1; i++)
            {
                playerOne.Board[rnd.Next(matrixDimension), rnd.Next(matrixDimension)] = BoardEnum.PositionState.Floating;
                playerTwo.Board[rnd.Next(matrixDimension), rnd.Next(matrixDimension)] = BoardEnum.PositionState.Floating;
            }


            // Keep asking the matrix until it reaches the maximum number of tries or all ships are found.
            do
            {
                // Change color depends on which player is playing 
                Console.ForegroundColor = currentPlayer.Color;

                Console.WriteLine("Player {0}\n", currentPlayer.Number);
                BoardEnum.PositionState currentSelectionBoardState = new BoardEnum.PositionState();

                bool validEntries = true;

                do
                {
                    // Valid entry when staring 
                    validEntries = true;

                    Console.WriteLine("Inform column position number (maximum {0}):", matrixDimension);
                    columnNumberSelected = int.Parse(Console.ReadLine());

                    Console.WriteLine("Inform line position number (maximum {0}):", matrixDimension);
                    lineNumberSelected = int.Parse(Console.ReadLine());

                    // Validate number entry
                    if (columnNumberSelected > matrixDimension || lineNumberSelected > matrixDimension)
                    {
                        validEntries = false;
                        Console.WriteLine("Invalid entry, maximum {0}:", matrixDimension);
                    }
                    else
                    {
                        // Set current board
                        currentSelectionBoardState = currentPlayer.Board[columnNumberSelected, lineNumberSelected];

                        if (currentSelectionBoardState == BoardEnum.PositionState.Attacked || currentSelectionBoardState == BoardEnum.PositionState.Sunk)
                            Console.WriteLine("------Position already attacked------");
                    }

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
                currentPlayer.Board[columnNumberSelected, lineNumberSelected] = currentSelectionBoardState;

                // Set who is the next player
                currentPlayer = currentPlayer.Number == PlayerEnum.Number.One ? playerTwo : playerOne;

                // The game finishes either when you reach the maximum number ships sunk
            } while (currentPlayer.NumberOfShipsLeft > 0);



            // Game finished and show the result of the game
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------------Well done Player {0}!---------------------------------\n\n", currentPlayer.Number);
            Console.Read();

        }
    }
}

