using FlareHR.Battleship.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FlareHR.Battleship.Tests
{
    // TODO: Implement unit testing for all methods of the services.

    [TestClass]
    public class AppUnitTest
    {
        // Mock services
        private readonly Mock<ILogger<IPlayerService>> mockLooger;
        private readonly GameService gameService;
        private readonly PlayerService playerService;

        public AppUnitTest()
        {
            // Mock services
            mockLooger = new Mock<ILogger<IPlayerService>>();
            playerService = new PlayerService(mockLooger.Object);
            gameService = new GameService(playerService);
        }

        [TestMethod]
        public void Check_DeployAttack_Is_Valid()
        {
            // Create Game 
            var game = gameService.CreateGame(10, 5);

            var columnInput = "10";
            var lineInput = "10";

            var attack = gameService.ValidateAttack(game, game.PlayerOne, columnInput, lineInput);

            // Validate if the entry is correct
            Assert.AreEqual(true, attack.Valid);
        }

        [TestMethod]
        public void Check_DeployAttack_Is_Invalid()
        {
            // Create Game 
            var game = gameService.CreateGame(10, 5);

            var columnInput = "A";// String to throw error
            var lineInput = "B";

            var attack = gameService.ValidateAttack(game, game.PlayerOne, columnInput, lineInput);

            // Validate if the entry is correct
            Assert.AreEqual(false, attack.Valid);
        }
    }
}
