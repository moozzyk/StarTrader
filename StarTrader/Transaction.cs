namespace StarTrader
{
	using System;

	public abstract class Transaction
	{
		protected Transaction(Player player, CommodityMarket market, int offerPrice, int quantity)
		{
			Player = player;
			Market = market;
			OfferPrice = Math.Min(CommodityMarket.MaxPrice, Math.Max(CommodityMarket.MinPrice, offerPrice));
			Quantity = quantity;
		}

		public Player Player { get; private set; }

		public CommodityMarket Market { get; private set; }

		public int OfferPrice { get; private set; }

		public int Quantity { get; private set; }

		public abstract int Execute(int allowedQuantity);
	}
}
