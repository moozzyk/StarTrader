namespace StarTrader
{
	using System.Collections.Generic;
	using System.Linq;

	class TransactionStage
	{
	    private readonly Game m_game;
		private readonly List<Transaction> m_commodityBids;

		public TransactionStage(Game game, List<Transaction> commodityBids)
		{
		    m_game = game;
			m_commodityBids = commodityBids;
		}

		public void PerformTransactions(CommodityMarket market)
		{
			market.PerformTransactions(m_commodityBids.Where(t => t.Market.Equals(market)));
		}

		public InvestmentStage NextStage()
		{
			// any bought commodities not moved to actual storage are forfeit
			var devnull = new CommodityStorage { Size = int.MaxValue };
			foreach (var player in m_game.Players)
			{
				foreach (var system in m_game.StarSystems.Values)
				{
					foreach (CommodityMarket market in system)
					{
						player.MoveBoughtCommodity(system, devnull, market.Commodity, int.MaxValue);
					}
				}
			}

			return new InvestmentStage();
		}
	}
}
