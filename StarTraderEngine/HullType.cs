namespace StarTrader
{
	using System;

	class HullAttribute : Attribute
	{
		public int ModuleCapacity { get; set; }

		public int Interception { get; set; }

		public int Crew { get; set; }

		public int Attack { get; set; }

		public int Defense { get; set; }

		/// <summary>
		/// Freight capacity
		/// </summary>
		public int Capacity { get; set; }

		public int Price { get; set; }

		public int RepairCost { get; set; }

		/// <summary>
		/// Determines if the ship can land on planet surface
		/// </summary>
		public bool Aerodynamic { get; set; }

		/// <summary>
		/// Implies illegal
		/// </summary>
		public bool Military { get; set; }

		internal static HullAttribute GetAttibute(HullType hullType)
		{
			return AttributeHelper<HullAttribute, HullType>.GetAttibute((int)hullType);
		}
	}

	public enum HullType
	{
		[Hull(ModuleCapacity = 1, Interception = 2, Crew = 1, Attack = 5, Defense = 4, Capacity = 0, Price = 250, RepairCost = 60, Aerodynamic = true, Military = true)]
		Dagger,

		[Hull(ModuleCapacity = 2, Interception = 1, Crew = 1, Attack = 2, Defense = 1, Capacity = 0, Price = 080, RepairCost = 10, Aerodynamic = true)]
		CorcoGamma,

		[Hull(ModuleCapacity = 3, Interception = 3, Crew = 1, Attack = 3, Defense = 3, Capacity = 0, Price = 225, RepairCost = 30, Aerodynamic = true)]
		Flute,

		[Hull(ModuleCapacity = 3, Interception = 3, Crew = 1, Attack = 5, Defense = 4, Capacity = 0, Price = 500, RepairCost = 80, Military = true)]
		Sword,

		[Hull(ModuleCapacity = 4, Interception = 1, Crew = 1, Attack = 1, Defense = 1, Capacity = 0, Price = 080, RepairCost = 10)]
		CorcoZeta,

		[Hull(ModuleCapacity = 5, Interception = 2, Crew = 1, Attack = 3, Defense = 2, Capacity = 0, Price = 160, RepairCost = 20)]
		Clarinet,

		[Hull(ModuleCapacity = 5, Interception = 1, Crew = 2, Attack = 5, Defense = 4, Capacity = 1, Price = 600, RepairCost = 100, Military = true)]
		Javelin,

		[Hull(ModuleCapacity = 6, Interception = 2, Crew = 1, Attack = 3, Defense = 3, Capacity = 1, Price = 190, RepairCost = 30)]
		CorcoYota,

		[Hull(ModuleCapacity = 6, Interception = 1, Crew = 1, Attack = 1, Defense = 1, Capacity = 2, Price = 085, RepairCost = 10)]
		Phoenix,

		[Hull(ModuleCapacity = 9, Interception = 1, Crew = 1, Attack = 2, Defense = 1, Capacity = 1, Price = 160, RepairCost = 30)]
		CorcoMu,

		[Hull(ModuleCapacity = 12, Interception = 1, Crew = 3, Attack = 1, Defense = 1, Capacity = 4, Price = 150, RepairCost = 15)]
		Monarch,

		[Hull(ModuleCapacity = 18, Interception = 1, Crew = 4, Attack = 1, Defense = 1, Capacity = 6, Price = 200, RepairCost = 20)]
		Leviathan,

		[Hull(ModuleCapacity = 0, Interception = 3, Crew = 1, Attack = 2, Defense = 2, Capacity = 0, Price = 70, RepairCost = 10, Aerodynamic = true)]
		Picolino,
	}
}