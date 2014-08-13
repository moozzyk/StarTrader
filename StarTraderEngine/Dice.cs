namespace StarTrader
{
	using System;

	static class Dice
	{
		private static readonly Random m_random = new Random(Environment.TickCount);

		public static int Roll()
		{
			int die1 = m_random.Next(1, 6);
			int die2 = m_random.Next(1, 6);

			return die1 + die2;
		}

		public static int Roll1Die()
		{
			return m_random.Next(1, 6);
		}
	}
}
