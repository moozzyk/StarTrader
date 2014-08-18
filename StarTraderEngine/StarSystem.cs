namespace StarTrader
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public class StarSystemAttribute : Attribute
	{
		public int SpacePortClass { get; set; }

		public int LegalSystem { get; set; }

		public int PoliceEfficiency { get; set; }

		public int SecurityLevel { get; set; }

		public bool ParkingHangar { get; set; }

		public bool Shipyard { get; set; }

		internal static StarSystemAttribute GetAttibute(StarSystemType starSystem)
		{
			return AttributeHelper<StarSystemAttribute, StarSystemType>.GetAttibute((int)starSystem);
		}
	}

	public enum StarSystemType
	{
		[StarSystem(SpacePortClass = 1, LegalSystem = 3, PoliceEfficiency = 4, SecurityLevel = 6, ParkingHangar = true)]
		GammaLeporis,

		[StarSystem(SpacePortClass = 3, LegalSystem = 4, PoliceEfficiency = 7, SecurityLevel = 9, ParkingHangar = true, Shipyard = true)]
		EpsilonEridani,

		[StarSystem(SpacePortClass = 3, LegalSystem = 2, PoliceEfficiency = 5, SecurityLevel = 7, ParkingHangar = true, Shipyard = true)]
		TauCeti,

		[StarSystem(SpacePortClass = 4, LegalSystem = 4, PoliceEfficiency = 8, SecurityLevel = 10, ParkingHangar = true, Shipyard = true)]
		BetaHydri,

		[StarSystem(SpacePortClass = 1, LegalSystem = 1, PoliceEfficiency = 2, SecurityLevel = 4)]
		SigmaDraconis,

		[StarSystem(SpacePortClass = 0, LegalSystem = 1, PoliceEfficiency = 0, SecurityLevel = 3)]
		MuHerculis,
	}

	public class StarSystem : IEnumerable<CommodityMarket>
	{
		private readonly string m_name;
		private readonly StarSystemAttribute m_type;
		private readonly IEnumerable<Commodity> m_allowedProduction;
		private readonly Dictionary<Commodity, CommodityMarket> m_markets = new Dictionary<Commodity, CommodityMarket>();
		private readonly Dictionary<Player, Warehouse> m_warehouses = new Dictionary<Player, Warehouse>();
		private readonly Dictionary<Player, List<Factory>> m_factories = new Dictionary<Player, List<Factory>>();
		private readonly Dictionary<StarSystem, int> m_hyperJumpSuccessChance = new Dictionary<StarSystem, int>();

		public StarSystem(StarSystemType type, IEnumerable<Commodity> allowedProduction, IEnumerable<CommodityMarket> markets)
		{
			m_name = type.ToString();
			m_type = StarSystemAttribute.GetAttibute(type);
			m_allowedProduction = allowedProduction;

			foreach (var market in markets)
			{
				market.System = this;
				m_markets[market.Commodity] = market;
			}
		}

		public string Name
		{
			get { return m_name; }
		}

		public CommodityMarket this[Commodity index]
		{
			get { return m_markets[index]; }
		}

		public Dictionary<StarSystem, int> HyperJumpSuccessChance
		{
			get { return m_hyperJumpSuccessChance; }
		}

		public List<Factory> GetFactories(Player player)
		{
			if (!m_factories.ContainsKey(player))
			{
				m_factories[player] = new List<Factory>();
			}

			return m_factories[player];
		}

		public Warehouse GetWarehouse(Player player)
		{
			if (!m_warehouses.ContainsKey(player))
			{
				// return empty
				m_warehouses[player] = new Warehouse(player);
			}

			return m_warehouses[player];
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

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = obj as StarSystem;
			return other != null && Name.Equals(other.Name);
		}

		public override string ToString()
		{
			return "System: " + Name;
		}
	}
}
