namespace StarTrader
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;

	public class InitiativeStage
	{
		private readonly Game m_game;
		private readonly Dictionary<Player, int> m_initiativeBids;
		private readonly List<Transaction> m_commodityBids;

		public InitiativeStage(Game game, Dictionary<Player, int> initiativeBids, List<Transaction> commodityBids)
		{
			m_game = game;
			m_initiativeBids = initiativeBids;
			m_commodityBids = commodityBids;
		}

		public void SetInitiative()
		{
			foreach (var player in m_game.Players)
			{
				player.Initiative = 0;
			}

			// handle duplicates
			bool retry;
			do
			{
				retry = false;
				var groups = m_game.Players.GroupBy(player => player.Initiative);
				foreach (var grouping in groups.Where(grouping => grouping.Count() > 1))
				{
					// redo the dice
					SetInitiativeForPlayers(grouping);
					retry = true;
				}
			}
			while (retry);

			// order players by initiative
			// TODO: this needs to be more explicit - the order may/will be lost when saved to DB
			// m_game.Players = m_game.Players.OrderBy(player => player.Initiative).ToList();

			// TODO - set the order from ui
			int current = 0;
			foreach (var player in m_game.Players)
			{
				player.Order = current++;
			}
		}

		public TransactionStage NextStage()
		{
			// TEMP
			return new TransactionStage(m_game, m_commodityBids);
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
