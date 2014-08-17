namespace StarTrader
{
    using System.Collections.Generic;

	public abstract class Scenario
	{
        internal abstract IEnumerable<Player> CreatePlayers(string[] playerNames);
	}
}
