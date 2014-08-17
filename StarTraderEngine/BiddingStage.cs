namespace StarTrader
{
	using System.Collections.Generic;

	public class BiddingStage
	{
		private readonly Game m_game;
		private readonly Dictionary<Player, int> m_initiativeBids = new Dictionary<Player, int>();
		private readonly List<Transaction> m_commodityBids = new List<Transaction>();

		public BiddingStage(Game game)
		{
			m_game = game;
		}

		public void InitiativeBid(Player player, int bid)
		{
			m_initiativeBids.Add(player, bid);
		}

		public void CommodityBid(Player player, CommodityMarket market, int offerPrice, int quantity, bool buy)
		{
			m_commodityBids.Add(buy ? (Transaction)new BuyTransaction(player, market, offerPrice, quantity) : new SellTransaction(player, market, offerPrice, quantity));
		}

		public InitiativeStage NextStage()
		{
			return new InitiativeStage(m_game, m_initiativeBids, m_commodityBids);
		}
	}
}
