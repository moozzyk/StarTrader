namespace StarTrader
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

	public class Game
	{
		public readonly Dictionary<StarSystemType, StarSystem> StarSystems;

	    public Game()
	    {
	        Turn = -1;
	        StarSystems = StarSystemFactory.CreateStarSystems();
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

            // TOOD: total initial ties or maybe initialize players outside before initializing the game
	    }

	    public int Turn { get; private set; }

        // TODO: eventually players should not be settable from outside
	    public List<Player> Players { get; internal set; }
	}
}
