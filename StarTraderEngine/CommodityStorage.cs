namespace StarTrader
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;

	public class CommodityStorage
	{
		// TODO - consider IStorable instead of hardcoded Commodity
		private readonly Dictionary<Commodity, int> m_storage = new Dictionary<Commodity, int>();

		public CommodityStorage()
		{
			foreach (var commodity in Enum.GetValues(typeof(Commodity)))
			{
				m_storage[(Commodity)commodity] = 0;
			}
		}

		public int Size { get; set; }

		public int Capacity
		{
			get { return StoragePerUnit * Size; }
		}

		virtual public int AvailableCapacity
		{
			get
			{
				int available = Capacity - m_storage.Sum(kvp => GetUsedCapacity(kvp.Key));
				Debug.Assert(available >= 0);
				return available;
			}
		}

		protected virtual int StoragePerUnit
		{
			get { return 10; }
		}

		virtual public int GetCount(Commodity commodity)
		{
			return m_storage[commodity];
		}

		/// <summary>
		/// Stores as much as possible, and returns the quantity actually stored
		/// </summary>
		virtual public int Store(Commodity commodity, int quantity)
		{
			int actuallyStored = commodity.RequiredCapacity() == 0 ? quantity : Math.Min(quantity, AvailableCapacity / commodity.RequiredCapacity());

			m_storage[commodity] += actuallyStored;
			Debug.Assert(AvailableCapacity >= 0);
			return actuallyStored;
		}

		virtual public int Remove(Commodity commodity, int quantity)
		{
			int actuallyRemoved = Math.Min(quantity, m_storage[commodity]);
			m_storage[commodity] -= actuallyRemoved;
			return actuallyRemoved;
		}

		public int MoveTo(CommodityStorage destination, Commodity commodity, int quantity)
		{
			quantity = Math.Min(GetCount(commodity), quantity);
			if (commodity.RequiredCapacity() > 0)
			{
				quantity = Math.Min(quantity, destination.AvailableCapacity / commodity.RequiredCapacity());
			}

			quantity = destination.Store(commodity, quantity);
			Remove(commodity, quantity);

			return quantity;
		}

		private int GetUsedCapacity(Commodity commodity)
		{
			return m_storage[commodity] * commodity.RequiredCapacity();
		}
	}
}
