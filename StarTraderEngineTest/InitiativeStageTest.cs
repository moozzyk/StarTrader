using System.Collections.Generic;
using System.Linq;

namespace StarTrader
{
	using Moq;
	using Xunit;

	public class InitiativeStageTest
	{
		[Fact]
		public void SetInitiative_Any_Ordered()
		{
			// arrange
			var game = new Game();
			game.Initialize(new[] { "player1", "player2", "player3" }, new FreeTradeScenario());
			var biddingStage = new BiddingStage(game);
			biddingStage.InitiativeBid(game.Players[0], 20);
			biddingStage.InitiativeBid(game.Players[1], 2);
			biddingStage.InitiativeBid(game.Players[2], 11);

			var stage = biddingStage.NextStage();

			// eliminate randomness
			var mockDice = new Mock<IDice>();
			mockDice.Setup(d => d.Roll()).Returns(5);

			// act
			stage.SetInitiative();

			// assert
			Assert.Equal(game.PlayersByInitiative.First().Name, "player1");
			Assert.Equal(game.PlayersByInitiative.Last().Name, "player2");
		}
	}
}
