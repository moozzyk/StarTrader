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
		private static readonly StarSystem GammaLeporis = new StarSystem(StarSystemType.GammaLeporis, new[] { Commodity.Polymer })
		{
			new CommodityMarket(Commodity.Polymer, 2, 1)
		};

		private static readonly StarSystem EpsilonEridani = new StarSystem(StarSystemType.EpsilonEridani, new Commodity[] { })
		{
			new CommodityMarket(Commodity.Polymer, 5, -7), 
			new CommodityMarket(Commodity.Isotope, 10, -7), 
			new CommodityMarket(Commodity.Food, 14, -8), 
			new CommodityMarket(Commodity.Component, 17, -9)
		};

		private static readonly StarSystem TauCeti = new StarSystem(StarSystemType.TauCeti, new[] { Commodity.Isotope })
		{
			new CommodityMarket(Commodity.Polymer, 6, -10),
			new CommodityMarket(Commodity.Isotope, 5, -9)
		};

		private static readonly StarSystem BetaHydri = new StarSystem(StarSystemType.BetaHydri, new Commodity[] { })
		{
			new CommodityMarket(Commodity.Polymer, 6, -8),
			new CommodityMarket(Commodity.Isotope, 10, -8),
			new CommodityMarket(Commodity.Food, 15, -9), 
			new CommodityMarket(Commodity.Component, 18, -10)
		};

		private static readonly StarSystem SigmaDraconis = new StarSystem(StarSystemType.SigmaDraconis, new[] { Commodity.Isotope, Commodity.Food })
		{
			new CommodityMarket(Commodity.Polymer, 8, -5),
			new CommodityMarket(Commodity.Isotope, 4, -1),
			new CommodityMarket(Commodity.Food, 10, -3),
			new CommodityMarket(Commodity.Component, 14, -8)
		};

		private static readonly StarSystem MuHerculis = new StarSystem(StarSystemType.MuHerculis, new[] { Commodity.Component })
		{
			new CommodityMarket(Commodity.Polymer, 8, -9),
			new CommodityMarket(Commodity.Isotope, 11, 1),
			new CommodityMarket(Commodity.Component, 12, -4)
		};

		private readonly string m_name;
		private readonly StarSystemAttribute m_type;
		private readonly IEnumerable<Commodity> m_allowedProduction;
		private readonly Dictionary<Commodity, CommodityMarket> m_markets = new Dictionary<Commodity, CommodityMarket>();
		private readonly Dictionary<Player, Warehouse> m_warehouses = new Dictionary<Player, Warehouse>();
		private readonly Dictionary<Player, List<Factory>> m_factories = new Dictionary<Player, List<Factory>>();
		private readonly Dictionary<StarSystem, int> m_hyperJumpSuccessChance = new Dictionary<StarSystem, int>();

		static StarSystem()
		{
			// TODO - convert to modifier table?
			GammaLeporis.HyperJumpSuccessChance[EpsilonEridani] = 6;
			GammaLeporis.HyperJumpSuccessChance[TauCeti] = 5;
			GammaLeporis.HyperJumpSuccessChance[BetaHydri] = 5;
			GammaLeporis.HyperJumpSuccessChance[SigmaDraconis] = 4;
			GammaLeporis.HyperJumpSuccessChance[MuHerculis] = 2;

			EpsilonEridani.HyperJumpSuccessChance[GammaLeporis] = 7;
			EpsilonEridani.HyperJumpSuccessChance[TauCeti] = 9;
			EpsilonEridani.HyperJumpSuccessChance[BetaHydri] = 8;
			EpsilonEridani.HyperJumpSuccessChance[SigmaDraconis] = 7;
			EpsilonEridani.HyperJumpSuccessChance[MuHerculis] = 6;

			TauCeti.HyperJumpSuccessChance[GammaLeporis] = 7;
			TauCeti.HyperJumpSuccessChance[EpsilonEridani] = 9;
			TauCeti.HyperJumpSuccessChance[BetaHydri] = 8;
			TauCeti.HyperJumpSuccessChance[SigmaDraconis] = 7;
			TauCeti.HyperJumpSuccessChance[MuHerculis] = 6;

			BetaHydri.HyperJumpSuccessChance[GammaLeporis] = 7;
			BetaHydri.HyperJumpSuccessChance[EpsilonEridani] = 8;
			BetaHydri.HyperJumpSuccessChance[TauCeti] = 8;
			BetaHydri.HyperJumpSuccessChance[SigmaDraconis] = 6;
			BetaHydri.HyperJumpSuccessChance[MuHerculis] = 6;

			SigmaDraconis.HyperJumpSuccessChance[GammaLeporis] = 4;
			SigmaDraconis.HyperJumpSuccessChance[EpsilonEridani] = 5;
			SigmaDraconis.HyperJumpSuccessChance[TauCeti] = 5;
			SigmaDraconis.HyperJumpSuccessChance[BetaHydri] = 4;
			SigmaDraconis.HyperJumpSuccessChance[MuHerculis] = 6;

			MuHerculis.HyperJumpSuccessChance[GammaLeporis] = 2;
			MuHerculis.HyperJumpSuccessChance[EpsilonEridani] = 4;
			MuHerculis.HyperJumpSuccessChance[TauCeti] = 4;
			MuHerculis.HyperJumpSuccessChance[BetaHydri] = 3;
			MuHerculis.HyperJumpSuccessChance[SigmaDraconis] = 5;
		}

		public StarSystem(StarSystemType type, IEnumerable<Commodity> allowedProduction)
		{
			m_name = type.ToString();
			m_type = StarSystemAttribute.GetAttibute(type);
			m_allowedProduction = allowedProduction;

            // TODO: 
            //Game.StarSystems[type] = this;
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
				return new Warehouse(player);
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

		private void Add(CommodityMarket market)
		{
			market.System = this;
			m_markets[market.Commodity] = market;
		}

		public static void Initialize()
		{
			// dummy method to trigger static initialization
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
