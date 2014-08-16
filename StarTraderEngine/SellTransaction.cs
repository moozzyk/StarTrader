namespace StarTrader
{
	using System;

	class SellTransaction : Transaction
	{
		public SellTransaction(Player player, CommodityMarket market, int offerPrice, int quantity) : base(player, market, offerPrice, quantity)
		{
		}

		public override int Execute(int allowedQuantity)
		{
			int actuallySold = Math.Min(allowedQuantity, Quantity);
			if (actuallySold > 0)
			{
				actuallySold = Player.SellCommodity(Market.System, Market.Commodity, OfferPrice, actuallySold);
			}

			return actuallySold;
		}
	}
}
