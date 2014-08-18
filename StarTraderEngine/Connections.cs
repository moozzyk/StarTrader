namespace StarTrader
{
	public abstract class Connections
	{
		private const int MaxTies = 10;
		private const int TiesIncrementCost = 10;
		private readonly Type m_type;

		public enum Type
		{
			Political,
			Economic,
			Criminal
		}

		protected Connections(Type type, int current)
		{
			m_type = type;
			Current = current;
		}

		public int Current { get; private set; }

		public bool Buy(Player player)
		{
			if (Current < MaxTies && player.Cash > TiesIncrementCost * (Current + 1))
			{
				Current++;
				player.Cash -= TiesIncrementCost * Current;
				AdjustReputation(player.Reputation);
				return true;
			}

			return false;
		}

		protected abstract void AdjustReputation(Reputation reputation);

		public class Political : Connections
		{
			public Political(int current)
				: base(Type.Political, current)
			{
			}

			protected override void AdjustReputation(Reputation reputation)
			{
				reputation.AdjustReputation(1);
			}
		}

		public class Economic : Connections
		{
			public Economic(int current)
				: base(Type.Economic, current)
			{
			}

			protected override void AdjustReputation(Reputation reputation)
			{
				reputation.AdjustReputation(2);
			}
		}

		public class Criminal : Connections
		{
			public Criminal(int current)
				: base(Type.Criminal, current)
			{
			}

			protected override void AdjustReputation(Reputation reputation)
			{
				reputation.AdjustReputation(-1);
			}
		}
	}
}