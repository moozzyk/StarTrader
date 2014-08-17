namespace StarTrader
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

	public class Game
	{
		public readonly Dictionary<StarSystemType, StarSystem> StarSystems = new Dictionary<StarSystemType, StarSystem>();

	    public Game()
	    {
	        Turn = -1;
	        // TODO: Initialize StarSystems here
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
	}
}
