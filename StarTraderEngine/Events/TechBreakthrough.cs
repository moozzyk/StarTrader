namespace StarTrader.Events
{
    using System;

    class TechBreakthrough : GameEvent
    {
        private const int MinModifier = -10;
        private readonly Commodity m_commodity;
        private readonly int m_supplyDemandModifier;

        public TechBreakthrough(Game game, int delay, Connections requiredConnections, Commodity commodity, int supplyDemandModifier)
            : base(game, delay, requiredConnections, false, "Technological breakthrough")
        {
            m_commodity = commodity;
            m_supplyDemandModifier = supplyDemandModifier;
        }

        protected override void Execute()
        {
            base.Execute();
            foreach (var starSystem in Game.StarSystems.Values)
            {
                foreach (CommodityMarket market in starSystem)
                {
                    if (market.Commodity != m_commodity)
                    {
                        continue;
                    }

                    if (m_supplyDemandModifier < 0)
                    {
                        market.SupplyDemandModifier = Math.Min(market.SupplyDemandModifier, Math.Max(MinModifier,
                            market.SupplyDemandModifier + m_supplyDemandModifier));
                    }
                    else
                    {
                        market.SupplyDemandModifier += m_supplyDemandModifier;
                    }
                }
            }
        }
    }
}