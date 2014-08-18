using System.Diagnostics;

namespace StarTrader
{
	using System;

	public enum Ties : int
	{
		Political,
		Economic,
		Criminal
	}

	public class Reputation
	{
		private const int Max = 40;
		private const int MaxTies = 10;
		private const int TiesIncrementCost = 10;

		private readonly int[] m_ties = new int[3];

		public Reputation(int reputation, int political, int economic, int criminal)
		{
			Current = reputation;
			m_ties[(int)Ties.Political] = political;
			m_ties[(int)Ties.Economic] = economic;
			m_ties[(int)Ties.Criminal] = criminal;
		}

		public int Current { get; private set; }

		public int PoliticalTies
		{
			get { return GetTies(Ties.Political); }
		}

		public int EconomicTies
		{
			get { return GetTies(Ties.Economic); }
		}

		public int CriminalTies
		{
			get { return GetTies(Ties.Criminal); }
		}

		public int BuyTies(Ties type, Player player)
		{
			int current = GetTies(type);
			if (current < MaxTies && player.Cash > TiesIncrementCost * (current + 1))
			{
				current++;
				SetTies(type, current);
				player.Cash -= TiesIncrementCost * current;

				int reputationAdjustment = 0;
				switch (type)
				{
					case Ties.Political:
						reputationAdjustment = 1;
						break;
					case Ties.Economic:
						reputationAdjustment = 2;
						break;
					case Ties.Criminal:
						reputationAdjustment = -1;
						break;
					default:
						Debug.Assert(false);
						break;
				}

				Current = Math.Max(1, Math.Min(Max, Current + reputationAdjustment));
			}

			return current;
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

		private int GetTies(Ties type)
		{
			return m_ties[(int)type];
		}

		private void SetTies(Ties type, int value)
		{
			m_ties[(int)type] = value;
		}
	}
}
