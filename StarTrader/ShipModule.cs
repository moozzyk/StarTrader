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

		public bool LifeSupport { get; set; }

		public bool Military { get; set; }

		internal static ModuleAttribute GetAttibute(ShipModule shipModule)
		{
			return AttributeHelper<ModuleAttribute, ShipModule>.GetAttibute((int)shipModule);
		}
	}

	public enum ShipModule
	{
		[Module(Attack = 7, Rockets = 14, Crew = 1, Price = 100, Military = true)]
		Arsenal,

		[Module(RequiredCapacity = 0, Price = 10)]
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
}