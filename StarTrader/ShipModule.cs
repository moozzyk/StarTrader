namespace StarTrader
{
	using System;

	class ModuleAttribute : Attribute
	{
		public const int RepairCost = 10;

		public ModuleAttribute()
		{
			RequiredCapacity = 1;
		}

		public int Attack { get; set; }

		public int Rockets { get; set; }

		public int Crew { get; set; }

		public int Price { get; set; }

		public int RequiredCapacity { get; set; }

		public int FreightCapacity { get; set; }

		public int SafeJumpModifier { get; set; }

		public bool LifeSupport { get; set; }

		public bool Military { get; set; }

		internal static ModuleAttribute GetAttibute(ShipModuleType shipModule)
		{
			return AttributeHelper<ModuleAttribute, ShipModuleType>.GetAttibute((int)shipModule);
		}
	}

	public enum ShipModuleType
	{
		[Module(Attack = 7, Rockets = 14, Crew = 1, Price = 100, Military = true)]
		Arsenal,

		[Module(RequiredCapacity = 0, Price = 10, SafeJumpModifier = 2)]
		SafeJump,

		[Module(Attack = 6, Crew = 2, Price = 50, Military = true)]
		CommUnit,

		[Module(FreightCapacity = 2, Price = 4)]
		Freight,

		[Module(Attack = 7, Rockets = 10, Crew = 1, Price = 80, Military = true)]
		HeavyWeapons,

		[Module(Attack = 4, Rockets = 7, Price = 30, Military = true)]
		Hunter,

		[Module(Attack = 3, Rockets = 5, Price = 17)]
		LightWeapons,

		[Module(Crew = 1, Price = 15, LifeSupport = true)]
		Passenger,
	}

	public class ShipModule : CommodityStorage
	{
		private readonly ShipModuleType m_type;
		private readonly ModuleAttribute m_module;

		public ShipModule(ShipModuleType type)
		{
			m_type = type;
			m_module = ModuleAttribute.GetAttibute(type);
			Size = m_module.FreightCapacity;
		}

		public ShipModuleType Type
		{
			get { return m_type; }
		}

		public int Attack
		{
			get { return m_module.Attack; }
		}

		public int Rockets
		{
			get { return m_module.Rockets; }
		}

		public int Crew
		{
			get { return m_module.Crew; }
		}

		public int Price
		{
			get { return m_module.Price; }
		}

		public int RequiredCapacity
		{
			get { return m_module.RequiredCapacity; }
		}

		public int SafeJumpModifier
		{
			get { return m_module.SafeJumpModifier; }
		}

		public bool LifeSupport
		{
			get { return m_module.LifeSupport; }
		}

		public bool Military
		{
			get { return m_module.Military; }
		}

		protected override int StoragePerUnit
		{
			get { return 2; }
		}
	}
}