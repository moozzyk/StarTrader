namespace StarTrader
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;

	public class Game
	{
		public readonly Dictionary<StarSystemType, StarSystem> StarSystems;
		public readonly List<GameEvent> CurrentEvents;
		public readonly List<GameEvent> AvailableEvents;
	    public readonly IDice Dice = new Dice.DiceImpl();

		public Game()
		{
			Turn = -1;
			StarSystems = StarSystemFactory.CreateStarSystems();
			CurrentEvents = new List<GameEvent>();
			AvailableEvents = GameEventFactory.CreateEvents(this);
		}

		public void Initialize(string[] playerNames, Scenario scenario)
		{
			Debug.Assert(playerNames != null && playerNames.Length > 0, "playerNames is null or empty");
			Debug.Assert(scenario != null, "scenario is null");

			if (Turn >= 0)
			{
				throw new InvalidOperationException("Game has already been initialized.");
			}

			Turn = 0;
			Players = scenario.CreatePlayers(playerNames).ToList();

			// TOOD: total initial ties
		}

		public int Turn { get; private set; }

		public List<Player> Players { get; private set; }

		public IEnumerable<Player> PlayersByInitiative { get { return Players.OrderByDescending(player => player.Initiative); } }
	}
}
