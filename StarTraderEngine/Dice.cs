namespace StarTrader
{
	using System;

	public interface IDice
	{
		int Roll();

		int Roll1Die();
	}

	public static class Dice
	{
		private static IDice m_instance = new DiceImpl();

		public static void SetTestingInstance(IDice dice)
		{
			m_instance = dice;
		}

		public static int Roll()
		{
			return m_instance.Roll();
		}

		public static int Roll1Die()
		{
			return m_instance.Roll1Die();
		}

		private class DiceImpl : IDice
		{
			private static readonly Random m_random = new Random(Environment.TickCount);

			public int Roll()
			{
				int die1 = m_random.Next(1, 6);
				int die2 = m_random.Next(1, 6);

				return die1 + die2;
			}

			public int Roll1Die()
			{
				return m_random.Next(1, 6);
			}
		}
	}
}
