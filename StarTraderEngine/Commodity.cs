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

        [CommodityDescriptor(RequiresLifeSupport = true)]
        Passengers,

        [CommodityDescriptor(RequiredCapacity = 1, BlackMarket = true)]
        Weapons,

        [CommodityDescriptor(BlackMarket = true)]
        Furs,

        [CommodityDescriptor(BlackMarket = true)]
        Drugs,

        [CommodityDescriptor(BlackMarket = true, RequiresLifeSupport = true)]
        Slaves,

        [CommodityDescriptor(RequiredCapacity = 5)]
        Module,

        [CommodityDescriptor(RequiredCapacity = 5, BlackMarket = true)]
        BlackMarketModule,
    }

    static public class CommodityHelper
    {
        public static int RequiredCapacity(this Commodity commodity)
        {
            return AttributeHelper<CommodityDescriptorAttribute, Commodity>.GetAttibute((int)commodity).RequiredCapacity;
        }

        public static bool BlackMarket(this Commodity commodity)
        {
            return AttributeHelper<CommodityDescriptorAttribute, Commodity>.GetAttibute((int)commodity).BlackMarket;
        }

        public static bool RequiresLifeSupport(this Commodity commodity)
        {
            return AttributeHelper<CommodityDescriptorAttribute, Commodity>.GetAttibute((int)commodity).RequiresLifeSupport;
        }
    }
}