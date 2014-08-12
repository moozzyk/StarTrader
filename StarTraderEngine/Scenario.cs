namespace StarTrader
{
	using System.Collections.Generic;

	class Scenario
	{
		public static readonly Scenario FreeTrade = new Scenario
		{
			StartingCash = 300, 
			StartingShips = new[]
			{
				new Spaceship(HullType.Clarinet, CrewClass.B)
				{
					new ShipModule(ShipModuleType.Freight), 
					new ShipModule(ShipModuleType.Freight), 
					new ShipModule(ShipModuleType.Freight),
					new ShipModule(ShipModuleType.Passenger),
					new ShipModule(ShipModuleType.LightWeapons),
					new ShipModule(ShipModuleType.SafeJump),
				}
			}
		};

		public int StartingCash { get; private set; }

		public IEnumerable<Spaceship> StartingShips { get; private set; }

		public void GetInitialTies(out int political, out int economic, out int criminal)
		{
			// TODO - get it from UI
			int sum = Dice.Roll();
			political = sum / 3;
			economic = sum / 3;
			criminal = sum - political - economic;
		}
	}
}
