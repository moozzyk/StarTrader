namespace StarTrader
{
	public class InvestmentStage
	{
		// TODO - ui can invoke player methods, do we need this class?

		public int BuyWarehouse(Player player, StarSystem system, int capacity)
		{
			return player.BuyWarehouse(system, capacity);
		}

		public void IncrementTies(Player player, Ties type)
		{
			player.Reputation.BuyTies(type, player);
		}
	}
}
