namespace StarTrader
{
	public class GameEvent
	{
		public GameEvent(int stage, int requiredTies, Ties tiesType, bool reusable, string description)
		{

		}

		public int Stage { get; set; }

		private static readonly GameEvent[] Events = new[]
		{
			// Opportunities
			new GameEvent(3, 4, Ties.Criminal, true, "Slaves"), // Buy(1) Mu Herculis, sell Epsilon Eridani.
			new GameEvent(2, 8, Ties.Criminal, true, "Weapons"), // Buy(3) Epsilon Eridani, sell Mu Herculis.
			new GameEvent(1, 1, Ties.Criminal, true, "Weapons"), // Buy(10) Tau Ceti, sell Gamma Leporis.
			new GameEvent(1, 1, Ties.Criminal, true,  "Combat comm unit"), // Comm unit available in Gamma Leporis.
			new GameEvent(2, 2, Ties.Economic, true, "Furs"), // Buy (10) SPACE PORT Mu Herkulis, sell (30) SPACE PORT Beta Hydri. Can buy 10 units, does not take cargo space.
			new GameEvent(2, 5, Ties.Criminal, true, "Slaves"), // Buy(2) Mu Herculis, sell Beta Hydri.
			new GameEvent(1, 6, Ties.Criminal, true, "Drugs"), // Buy(15) Beta Hydri, sell Gamma Leporis.
			new GameEvent(1, 5, Ties.Political, true, "Envoy transport"), // Transport the envoy and his companions from SPACE PORT Epsilon Eridani to SPACE PORT Mu Herkulis. Requires 1 passenger module. Requires REPUTATION 20 or more. Receives 200 HT after arrival to Mu Herkulis.
			new GameEvent(2,10, Ties.Economic, true, "Unknown alien plants"), // Plants to be picked up from SPACE PORT Gamma Leporis and delivered to SPACE PORT Epsilon Eridani. Use 1 Freight module (can't be stored in hull storage). Receives 150 HT after delivery and adds 3 to REPUTATION.
			new GameEvent(1, 10, Ties.Political, true, "Slaves"), // Buy(1) Mu Herculis, sell Gamma Leporis.
			new GameEvent(3, 3, Ties.Criminal, true,  "Slaves"), // Buy(2) Mu Herculis, sell Gamma Leporis.
			new GameEvent(1, 6, Ties.Criminal, true,  "Drugs"), // Buy(12) Epsilon Eridani, sell Gamma Leporis.
			new GameEvent(1,10, Ties.Criminal, true,  "Hull Javelin"), // Hull Javels and/or 5 black market modules. Available in Beta Hydri.
			new GameEvent(4 ,8, Ties.Criminal, true,  "Black market modules" ), // One each type. Available on Tau Ceti.
			new GameEvent(2, 2, Ties.Political, true, "Research expedition"), // Organized by independent corporation. Player can send 1 legal ship. 
							// Roll 1D and move the ship to the stage ahead by the number rolled - that's the stage when the ship returns. if rolled 1, the ship is destroyed.
							// After the return roll 2D and multiple by 50 for the reward
			new GameEvent(4, 3, Ties.Economic, true, "Hull Dagger"), // Hull Dagger and/or 1 black market module. Available on Tau Ceti.
			new GameEvent(3, 8, Ties.Economic, true, "Weapons"), // Buy(8) Epsilon Eridani, sell Gamma Leporis.
			new GameEvent(2,2, Ties.Economic, true, "Weapons"), // Buy(3) Beta Hydri, sell Gamma Leporis.
			new GameEvent(2 ,8, Ties.Criminal, true,  "Drugs"), // Buy(10) Tau Ceti, sell Sigma Draconis.
			new GameEvent(2,10, Ties.Economic, true, "Any black market hull"), // Any black market hull and/or 5 modules. Available on Mu Herkulis.
			new GameEvent(2 ,8, Ties.Criminal, true,  "Hull Sword" ), // Hull Sword and/or 3 black market modules. Available on Mu Herkulis.
			new GameEvent(1, 5, Ties.Economic, true, "Unique animals"), // Pick up from SPACE PORT Mu Herkulis and delivery to SPACE PORT Epsilon Eridani. Require 1 passenger module. Reward after arrival - 150 HT.
			new GameEvent(2 ,5, Ties.Criminal, true,  "Weapons"), // Buy(3) Epsilon Eridani, sell Mu Herculis.
			new GameEvent(3,4, Ties.Criminal, true,  "Module Dagger"), // Hull Dagger and/or 1 black market moduł. Available on Sigma Draconis.
			new GameEvent(1, 7, Ties.Political, true, "Weapons"), // Buy(5) Beta Hydri, sell Mu Herculis.
			new GameEvent(1, 5, Ties.Political, true, "Black market modules"), // One each type. Available on Epsilon Eridani.

			// Events
			new GameEvent(1, 5, Ties.Economic, false, "Psychic disruption"), // Substract 4 from dice rolls in hyperjump phase
			new GameEvent(1, 10, Ties.Political, true, "Alien race"), // Border planets under attack. Panic ensues. All prices go down by 3.
			new GameEvent(4, 3, Ties.Political, false, "Galactic war"), // Component prices +3. Polymers +6. Isotopes +2. During this stage (and next) ships cannot be bought (including black market). Ignore applicable events. Add 5 to the die roll for spaceship sellers during these stages. Increment Police Efficiency by 4 during this stage.
			new GameEvent(3, 3, Ties.Criminal,  true, "Inflation"), // Fake bank notes flood the market. Cut everyone's cash by 50% (round up). Also cut all unpaid loans (don't change interest).
			new GameEvent(2, 2, Ties.Political, false, "Civil war"), // Civil war in Gamma Leporis. Weapons prices sold on the Gamma Leporis planet increase 3x. All ships in that space port and all factories and warehouses on Gamma Leporis are nationalized - discard them. Owners are compensated at 50% current value of the factories, warehouse prices and catalog price of the ships (hull and modules, not the crew). Commodities are lost.
			new GameEvent(4, 3, Ties.Political, false, "Colony"), // New planet in Mu Herkulis. All prices in the system go up by 5.
			new GameEvent(2, 6, Ties.Economic, true, "Technological breakthrough"), // In components. All Supply/demand modifiers go up by 3 through the end of the game.
			new GameEvent(3, 8, Ties.Economic, true, "Technological breakthrough"), // Food production; synthetic food is available. All Supply/demand modifiers go up by 2 through the end of the game.
			new GameEvent(2, 6, Ties.Economic, true, "Technological breakthrough"), // Isotope production. All Supply/demand modifiers go down by 3 through the end of the game, can't be lower than -10.
			new GameEvent(1, 7, Ties.Political, true, "Epidemic"), // Prices increase by 4. Can't hyperjump to and from space ports (only to/from the plant and space). Police efficiency and security level increase by during this stage.
			new GameEvent(4, 8, Ties.Political, false, "Inspection"), // Federal govt interrogate everyone. Decrement everyone's Reputation by 2D (each player rolls separately).
			new GameEvent(1, 10, Ties.Criminal,  false, "Pirate attack"), // On Mu Herkulis. All commodities and modules in warehouses are lost. Roll 1D for each ship in space port in Mu Herkulis. If <= 3 - the ship and everything onboard is lost, >=4 ship escapes into inter-planetary space. Police efficiency and Security level go up by 3 during this stage.
			new GameEvent(4, 3, Ties.Political, false, "Special tax"), // Every player immediately pays 1 HT for each warehouse capacity unit, 2 HT for each unit of factory capacity and 5 HT for each space ship. Up to available cash.
			new GameEvent(4, 3, Ties.Economic, true, "Discovery"), // New star system with developed polymer production. Polymer prices -5. Component and isotope +2.
		};
	}
}
