namespace StarTrader
{
	using System;

	public class Reputation
	{
		private const int Max = 40;

		public Reputation(int reputation, int political, int economic, int criminal)
		{
			Current = reputation;
			PoliticalTies = political;
			EconomicTies = economic;
			CriminalTies = criminal;
		}

		public int Current { get; set; }

		public int PoliticalTies { get; set; }
		
		public int EconomicTies { get; set; }
		
		public int CriminalTies { get; set; }

		/// <summary>
		/// Called during Control Stage
		/// </summary>
		/// <returns>Cash bonus, if any</returns>
		public int Adjust()
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

			return bonus;
		}
	}
}
