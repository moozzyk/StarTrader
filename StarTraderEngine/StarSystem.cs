namespace StarTrader
{
	using System.Collections;
	using System.Collections.Generic;

	class StarSystem : IEnumerable<CommodityMarket>
	{
		private static readonly StarSystem GammaLeporis = new StarSystem("GammaLeporis", 1, 3, 4, 6, new[] { Commodity.Polymer }) { new CommodityMarket(Commodity.Polymer, 2, 1) };
		private static readonly StarSystem EpsilonEridani = new StarSystem("EpsilonEridani", 3, 4, 7, 9, new Commodity[] { }) { new CommodityMarket(Commodity.Polymer, 5, -7), new CommodityMarket(Commodity.Isotope, 10, -7), new CommodityMarket(Commodity.Food, 14, -8), new CommodityMarket(Commodity.Component, 17, -9) };

		private readonly string m_name;
		private readonly int m_spacePortClass;
		private readonly int m_legalSystem;
		private readonly int m_policeEfficiency;
		private readonly int m_securityLevel;
		private readonly IEnumerable<Commodity> m_allowedProduction;
		private readonly Dictionary<Commodity, CommodityMarket> m_markets = new Dictionary<Commodity, CommodityMarket>();
		private readonly Dictionary<Player, Warehouse> m_warehouses = new Dictionary<Player, Warehouse>();
		private readonly Dictionary<Player, Factory> m_factories = new Dictionary<Player, Factory>();
		private readonly Dictionary<StarSystem, int> m_hyperJumpSuccessChance = new Dictionary<StarSystem, int>();

		static StarSystem()
		{
			GammaLeporis.HyperJumpSuccessChance[EpsilonEridani] = 6;
			EpsilonEridani.HyperJumpSuccessChance[GammaLeporis] = 7;
			// TODO - the rest
		}

		public StarSystem(string name, int spacePortClass, int legalSystem, int policeEfficiency, int securityLevel, IEnumerable<Commodity> allowedProduction)
		{
			m_name = name;
			m_spacePortClass = spacePortClass;
			m_legalSystem = legalSystem;
			m_policeEfficiency = policeEfficiency;
			m_securityLevel = securityLevel;
			m_allowedProduction = allowedProduction;
		}

		public string Name
		{
			get { return m_name; }
		}

		public Dictionary<StarSystem, int> HyperJumpSuccessChance
		{
			get { return m_hyperJumpSuccessChance; }
		}

		public Dictionary<Player, Factory> Factories
		{
			get { return m_factories; }
		}

		public Dictionary<Player, Warehouse> Warehouses
		{
			get { return m_warehouses; }
		}

		// IEnumerable
		IEnumerator<CommodityMarket> IEnumerable<CommodityMarket>.GetEnumerator()
		{
			return m_markets.Values.GetEnumerator();
		}

		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable<CommodityMarket>)this).GetEnumerator();
		}

		private void Add(CommodityMarket market)
		{
			m_markets[market.Commodity] = market;
		}
	}
}
