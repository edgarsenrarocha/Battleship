using System;

namespace FlareHR.Battleship.Entities
{
    public class Game
    {
        public Game()
        {
            if (Columns != Lines)
                throw new Exception("Columns and Lines must be equal.");
        }

        public int Columns { get; set; }
        public int Lines { get; set; }

        public int NumberOfShips { get; set; }

        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
    }
}
