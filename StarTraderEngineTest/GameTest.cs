namespace StarTrader
{
    using Moq;
    using System;
    using Xunit;

    public class GameTest
    {
        [Fact]
        public void Turn_set_to_negative_one_for_new_game()
        {
            Assert.Equal(-1, new Game().Turn);
        }

        [Fact]
        public void Initialize_sets_turn_to_0()
        {
            var game = new Game();
            game.Initialize(new string[1], Mock.Of<Scenario>());

            Assert.Equal(0, game.Turn);
        }

        [Fact]
        public void Initialize_uses_scenario_to_create_players()
        {
            var mockScenario = new Mock<Scenario>();
            var playerNames = new string[1];
            new Game().Initialize(playerNames, mockScenario.Object);

            mockScenario.Verify(s => s.CreatePlayers(playerNames), Times.Once);
        }

        [Fact]
        public void Cannot_initialize_twice()
        {
            var game = new Game();
            game.Initialize(new string[1], Mock.Of<Scenario>());

            Assert.Throws<InvalidOperationException>(() => game.Initialize(new string[1], Mock.Of<Scenario>()));
        }
    }
}
