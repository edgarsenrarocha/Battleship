using FlareHR.Battleship.ConsoleApp.Enums;
using System;

namespace FlareHR.Battleship.ConsoleApp.Models
{
    public class Player
    {
        public PlayerEnum.Number Number { get; set; }
        public int NumberOfShipsLeft { get; set; }
        public BoardEnum.PositionState[,] Board { get; set; }
        public ConsoleColor Color { get; set; }      
    }
}
