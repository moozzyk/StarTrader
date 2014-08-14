namespace StarTrader
{
	using System.Collections.Generic;

	public static class Game
	{
		public static Scenario Scenario;
		public static readonly Dictionary<StarSystemType, StarSystem> StarSystems = new Dictionary<StarSystemType, StarSystem>();

		static Game()
		{
			// TODO
			Scenario = Scenario.FreeTrade;
			StarSystem.Initialize();
		}

		public static List<Player> Players { get; set; }
	}
}
