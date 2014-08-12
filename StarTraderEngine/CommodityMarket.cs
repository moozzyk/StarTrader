namespace StarTrader
{
	using System.Collections.Generic;

	class CommodityMarket
	{
		private readonly List<Player> m_retailers = new List<Player>();
		private readonly List<Player> m_wholesalers = new List<Player>();

		public CommodityMarket(Commodity commodity, int price, int modifier)
		{
			Commodity = commodity;
			Price = price;
			SupplyDemandModifier = modifier;
		}

		public Commodity Commodity { get; private set; }

		public int Price { get; private set; }

		public int SupplyDemandModifier { get; private set; }

		public Player Dictator { get; private set; }

		public IEnumerable<Player> Retailers
		{
			get { return m_retailers; }
		}

		public IEnumerable<Player> Wholesalers
		{
			get { return m_wholesalers; }
		}
	}
}
