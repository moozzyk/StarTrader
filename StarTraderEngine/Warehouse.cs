namespace StarTrader
{
	public class Warehouse : CommodityStorage
	{
		public const int Price = 20;

		public Warehouse(Player owner)
		{
			Owner = owner;
		}

		public Player Owner { get; private set; }

		public override int Store(Commodity commodity, int quantity)
		{
			if (commodity.BlackMarket())
			{
				// can't store illegal materials in warehouses
				return 0;
			}

			return base.Store(commodity, quantity);
		}
	}
}
