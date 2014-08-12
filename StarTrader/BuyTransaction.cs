namespace StarTrader
{
	using System;

	class BuyTransaction : Transaction
	{
		public BuyTransaction(Player player, CommodityMarket market, int offerPrice, int quantity) : base(player, market, offerPrice, quantity)
		{
		}

		public override int Execute(int allowedQuantity)
		{
			int actuallyBought = Math.Min(allowedQuantity, Quantity);
			if (actuallyBought > 0)
			{
				actuallyBought = Player.BuyCommodity(Market.System, Market.Commodity, OfferPrice, actuallyBought);
			}

			return actuallyBought;
		}
	}
}
