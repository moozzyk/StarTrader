namespace StarTrader
{
	using System.Collections.Generic;
	using System.Linq;

	public class TransactionStage
	{
		private readonly List<Transaction> m_commodityBids;

		public TransactionStage(List<Transaction> commodityBids)
		{
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
			foreach (var player in Game.Players)
			{
				foreach (var system in Game.StarSystems.Values)
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
