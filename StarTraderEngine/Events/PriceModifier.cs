namespace StarTrader.Events
{
    using System;
    using System.Linq;

    internal class PriceModifier : GameEvent
    {
        private readonly Func<Commodity, int> m_priceModifier;
        private readonly Func<StarSystemType, bool> m_starSystemFilter;

        public PriceModifier(Game game, int delay, Connections requiredConnections, bool reusable, string description, Func<Commodity, int> priceModifier, Func<StarSystemType, bool> starSystemFilter)
            : base(game, delay, requiredConnections, reusable, description)
        {
            m_priceModifier = priceModifier;
            m_starSystemFilter = starSystemFilter ?? (s => true);
        }

        protected override void Execute()
        {
            base.Execute();
            foreach (CommodityMarket market in Game.StarSystems.Values.Where(s => m_starSystemFilter(s.Type)).SelectMany(starSystem => starSystem))
            {
                market.AdjustPrice(m_priceModifier(market.Commodity));
            }
        }
    }
}