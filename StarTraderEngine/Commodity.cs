namespace StarTrader
{
	using System;

	public class CommodityDescriptorAttribute : Attribute
	{
		public int RequiredCapacity { get; set; }

		public bool BlackMarket { get; set; }

		public bool RequiresLifeSupport { get; set; }
	}

	public enum Commodity
	{
		[CommodityDescriptor(RequiredCapacity = 1)]
		Polymer,

		[CommodityDescriptor(RequiredCapacity = 1)]
		Isotope,

		[CommodityDescriptor]
		Component,

		[CommodityDescriptor]
		Food,

		[CommodityDescriptor(BlackMarket = true)]
		Drugs,

		[CommodityDescriptor(RequiredCapacity = 1, BlackMarket = true)]
		Weapons,

		[CommodityDescriptor(RequiresLifeSupport = true)]
		Passengers,

		[CommodityDescriptor(BlackMarket = true, RequiresLifeSupport = true)]
		Slaves,
	}
}