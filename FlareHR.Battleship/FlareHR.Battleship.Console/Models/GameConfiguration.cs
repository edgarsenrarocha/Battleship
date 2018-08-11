using System;
using System.Collections.Generic;
using System.Text;

namespace FlareHR.Battleship.ConsoleApp.Models
{
    public class GameConfiguration
    {
        public GameConfiguration()
        {
            // Default values
            NumberOfShips = 5;

            if (Columns != Lines)
                throw new Exception("Columns and Lines must be equal.");
        }

        public int Columns { get; set; }
        public int Lines { get; set; }
        public int NumberOfShips { get; set; }
    }
}
