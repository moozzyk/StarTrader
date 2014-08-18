namespace StarTrader
{
	public class InfiniteStorage : CommodityStorage
	{
		public InfiniteStorage()
		{
			Size = 10; // this doesn't matter
		}

		public override int AvailableCapacity
		{
			get { return int.MaxValue; }
		}
	}
}