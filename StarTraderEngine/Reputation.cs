namespace StarTrader
{
	using System;

	public class Reputation
	{
		private const int Max = 40;

		private readonly Connections m_political;
		private readonly Connections m_economic;
		private readonly Connections m_criminal;

		public Reputation(int reputation, int political, int economic, int criminal)
		{
			Current = reputation;
			m_political = new Connections.Political(political);
			m_economic = new Connections.Economic(economic);
			m_criminal = new Connections.Criminal(criminal);
		}

		public int Current { get; private set; }

		public Connections PoliticalConnections
		{
			get { return m_political; }
		}

		public Connections EconomicTies
		{
			get { return m_economic; }
		}

		public Connections CriminalTies
		{
			get { return m_criminal; }
		}

		public void AdjustReputation(int adjustment)
		{
			// TODO - reconsider this method when control stage is implemented
			Current = Math.Max(1, Math.Min(Max, Current + adjustment));
		}

		/// <summary>
		/// Called during Control Stage
		/// </summary>
		/// <returns>Cash bonus, if any</returns>
		public void Adjust(Player player)
		{
			int bonus = 0;

			// TODO - make it a lookup table?
			if (Current == Max)
			{
				bonus = 40;
			}
			else if (Current >= 39)
			{
				bonus = 35;
			}
			else if (Current >= 36)
			{
				bonus = 30;
			}
			else if (Current >= 34)
			{
				bonus = 25;
			}
			else if (Current >= 30)
			{
				bonus = 20;
			}
			else if (Current >= 29)
			{
				bonus = 15;
			}
			else if (Current >= 27)
			{
				bonus = 10;
			}
			else if (Current >= 25)
			{
				bonus = 5;
			}

			if (Current >= 1 && Current <= 19)
			{
				Current = Math.Min(Current + 3, 20);
			}

			player.Cash += bonus;
		}
	}
}
