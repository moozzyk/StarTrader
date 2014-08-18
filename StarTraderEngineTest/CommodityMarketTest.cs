namespace StarTraderTest
{
	using Xunit;

	using StarTrader;

	public class CommodityMarketTest
	{
		[Fact]
		public void PerformTransactions_MultipleOffers_Execute()
		{
			// arrange
			Dice.SetTestingInstance(new DiceFake(10));
			var playerA = new Player("a", 300, 20, 1, 1, 1);
			var playerB = new Player("b", 300, 20, 1, 1, 1);
			var playerC = new Player("c", 300, 20, 1, 1, 1);
			var componentMarket = new CommodityMarket(Commodity.Component, 13, -4);
			var muHerculis = new StarSystem(StarSystemType.MuHerculis, new Commodity[0], new[] { componentMarket });
			var playerABuy = new BuyTransaction(playerA, componentMarket, 12, 100);
			var playerBBuy = new BuyTransaction(playerB, componentMarket, 16, 7);
			var playerCSell = new SellTransaction(playerC, componentMarket, 10, 3);
			playerC.BuyWarehouse(muHerculis, 1);
			Assert.Equal(280, playerC.Cash);
			muHerculis.GetWarehouse(playerC).Store(Commodity.Component, 3);
			Assert.Equal(3, muHerculis.GetWarehouse(playerC).GetCount(Commodity.Component));

			// act
			componentMarket.PerformTransactions(new Transaction[] { playerABuy, playerBBuy, playerCSell });

			// assert
			Assert.Equal(0, muHerculis.GetWarehouse(playerC).GetCount(Commodity.Component));
			Assert.Equal(310, playerC.Cash);

			var devnull = new InfiniteStorage();
			int boughtB = playerB.MoveBoughtCommodity(muHerculis, devnull, Commodity.Component, 100);
			Assert.Equal(7, boughtB);
			Assert.Equal(188, playerB.Cash);

			int boughtA = playerA.MoveBoughtCommodity(muHerculis, devnull, Commodity.Component, 100);
			Assert.Equal(0, boughtA);
			Assert.Equal(300, playerA.Cash);

			Assert.Equal(12, componentMarket.Price);
		}

		[Fact]
		public void PerformTransactions_MultipleOffers_InitiativeWins()
		{
			// arrange
			Dice.SetTestingInstance(new DiceFake(10));
			var playerA = new Player("a", 300, 20, 1, 1, 1);
			var playerB = new Player("b", 300, 20, 1, 1, 1);
			playerA.Initiative = 100;
			playerB.Initiative = 1;
			var componentMarket = new CommodityMarket(Commodity.Component, 13, -4);
			var muHerculis = new StarSystem(StarSystemType.MuHerculis, new Commodity[0], new[] { componentMarket });
			var playerABuy = new BuyTransaction(playerA, componentMarket, 16, 8);
			var playerBBuy = new BuyTransaction(playerB, componentMarket, 16, 7);

			// act
			componentMarket.PerformTransactions(new Transaction[] { playerABuy, playerBBuy });

			// assert
			var devnull = new InfiniteStorage();

			// player A has higher initiative, so it managed to buy first
			int boughtA = playerA.MoveBoughtCommodity(muHerculis, devnull, Commodity.Component, 100);
			Assert.Equal(8, boughtA);
			Assert.Equal(172, playerA.Cash);

			int boughtB = playerB.MoveBoughtCommodity(muHerculis, devnull, Commodity.Component, 100);
			Assert.Equal(6, boughtB);
			Assert.Equal(204, playerB.Cash);

			// after 2 buys the price goes up
			Assert.Equal(16, componentMarket.Price);
		}

		private class DiceFake : IDice
		{
			private readonly int m_result;

			public DiceFake(int result)
			{
				m_result = result;
			}

			public int Roll()
			{
				return m_result;
			}

			public int Roll1Die()
			{
				return m_result;
			}
		}
	}
}
