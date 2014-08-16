namespace StarTrader
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;

	class InitiativeStage
	{
		private readonly Dictionary<Player, int> m_initiativeBids;
		private readonly List<Transaction> m_commodityBids;

		public InitiativeStage(Dictionary<Player, int> initiativeBids, List<Transaction> commodityBids)
		{
			m_initiativeBids = initiativeBids;
			m_commodityBids = commodityBids;
		}

		public void SetInitiative()
		{
			foreach (var player in Game.Players)
			{
				player.Initiative = 0;
			}

			// handle duplicates
			bool retry;
			do
			{
				retry = false;
				var groups = Game.Players.GroupBy(player => player.Initiative);
				foreach (var grouping in groups.Where(grouping => grouping.Count() > 1))
				{
					// redo the dice
					SetInitiativeForPlayers(grouping);
					retry = true;
				}
			}
			while (retry);

			// order players by initiative
			Game.Players = Game.Players.OrderBy(player => player.Initiative).ToList();

			// TODO - set the order from ui
			int current = 0;
			foreach (var player in Game.Players)
			{
				player.Order = current++;
			}
		}

		public TransactionStage NextStage()
		{
			// TEMP
			return new TransactionStage(m_commodityBids);
		}

		private void SetInitiativeForPlayers(IEnumerable<Player> players)
		{
			foreach (var player in players)
			{
				int cost;
				m_initiativeBids.TryGetValue(player, out cost);
				Debug.Assert(cost >= 0); // don't handle negatives

				// on subsequent iterations inititaive must be >= 2
				if (player.Initiative == 0)
				{
					// adjust and pay once
					cost = Math.Min(cost, player.Cash);
					player.Cash -= cost;
					m_initiativeBids[player] = cost;
				}

				player.Initiative = cost + Dice.Roll();
			}
		}
	}
}
