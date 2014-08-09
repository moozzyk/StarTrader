namespace StarTrader
{
	using System.Collections;
	using System.Collections.Generic;

	class Scenario
	{
	    public static readonly Scenario FreeTrade = new Scenario
	    {
	        StartingCash = 300,
	        StartingShips =
	            new[]
	            {
	                new Spaceship(HullType.Clarinet, CrewClass.B)
	                {
	                    ShipModule.Freight,
	                    ShipModule.Freight,
	                    ShipModule.Freight,
	                    ShipModule.Passenger,
	                    ShipModule.LightWeapons,
	                    ShipModule.SafeJump
	                }
	            }
	    };

		public int StartingCash { get; private set; }

		public IEnumerable<Spaceship> StartingShips { get; private set; }
	}
}
