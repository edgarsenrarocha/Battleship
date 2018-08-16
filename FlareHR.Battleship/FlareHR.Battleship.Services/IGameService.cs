using FlareHR.Battleship.Entities;

namespace FlareHR.Battleship.Services
{
    public interface IGameService
    {
        Attack ValidateAttack(Game game, Player player, string columnInput, string lineInput);

        Game CreateGame(int dimension, int numberOfShips);

        bool ValidateInput(string input, int minValue, int maxValue, out int value);
    }
}
