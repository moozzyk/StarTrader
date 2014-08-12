namespace StarTrader
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;

	class Warehouse
	{
		private readonly Dictionary<Commodity, int> m_storage = new Dictionary<Commodity, int>();

		public Warehouse(Player owner)
		{
			Owner = owner;
			foreach (var commodity in (Commodity[])Enum.GetValues(typeof(Commodity)))
			{
				m_storage[commodity] = 0;
			}
		}

		public Player Owner { get; private set; }

		public int Capacity { get; set; }

		public int Available
		{
			get
			{
				int available = Capacity - m_storage.Values.Sum();
				Debug.Assert(available >= 0);
				return available;
			}
		}

		public int GetCount(Commodity commodity)
		{
			return m_storage[commodity];
		}

		public void Store(Commodity commodity, int quantity)
		{
			if (quantity > Available)
			{
				throw new InvalidOperationException("Requested quantity is greater than available capacity");
			}

			m_storage[commodity] += quantity;
		}

		public int Remove(Commodity commodity, int quantity)
		{
			if (quantity > GetCount(commodity))
			{
				throw new InvalidOperationException("Requested quantity is greater than available");
			}

			m_storage[commodity] -= quantity;

		    return m_storage[commodity];
		}
	}
}
