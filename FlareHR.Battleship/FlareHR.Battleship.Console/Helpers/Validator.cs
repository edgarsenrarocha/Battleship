using System;
using System.Collections.Generic;
using System.Text;

namespace FlareHR.Battleship.ConsoleApp.Helpers
{
    public static class Validator
    {
        /// <summary>
        /// Validate Input from console
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ValidateInput(string input, out int value)
        {
            bool isValid = int.TryParse(input, out value);

            // There is no position 0
            if (isValid && value == 0)
                return false;

            return isValid;
        }
    }
}
