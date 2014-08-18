namespace StarTrader
{
	using System.Linq;

	using Xunit;

	public class FreeTradeScenarioTest
	{
		[Fact]
		public void Players_created_with_default_settings()
		{
			var players = new FreeTradeScenario().CreatePlayers(new[] { "p1", "p2" }).ToArray();

			Assert.Equal(2, players.Length);
			Assert.True(players.All(p => p.Cash == 300));
			Assert.True(players.All(p => p.Reputation.Current == 20));
			Assert.True(players.All(p => p.Reputation.CriminalTies == -1));
			Assert.True(players.All(p => p.Reputation.EconomicTies == -1));
			Assert.True(players.All(p => p.Reputation.PoliticalTies == -1));
		}
	}
}
