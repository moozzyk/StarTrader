namespace StarTrader
{
	using System;

	class Reputation
	{
		private const int Max = 40;
		private const int Initial = 20;

		public Reputation()
		{
			Current = Initial;
		}

		public int Current { get; private set; }

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
				Current = Math.Min(Current + 3, Initial);
			}

			return bonus;
		}
	}
}
