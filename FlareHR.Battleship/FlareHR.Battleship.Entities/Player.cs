using FlareHR.Battleship.Entities.Enums;
using System;

namespace FlareHR.Battleship.Entities
{
    public class Player
    {
        public PlayerNumber Number { get; set; }
        public int NumberOfShipsLeft { get; set; }
        public PositionState[,] Board { get; set; }
        public PositionState BoardLastState { get; set; }
        public ConsoleColor Color { get; set; }
    }
}
