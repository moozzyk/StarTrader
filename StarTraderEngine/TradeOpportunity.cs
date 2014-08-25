using System;
using System.Diagnostics;

namespace StarTrader
{
    public class TradeOpportunity : Opportunity
    {
        private readonly Commodity m_commodity;
        private readonly int m_unitCost;

        public TradeOpportunity(Game game, int delay, Connections requiredConnections, bool reusable, string description, Commodity commodity, int unitCost, StarSystemType source)
            : base(game, delay, requiredConnections, reusable, description, source)
        {
            m_commodity = commodity;
            m_unitCost = unitCost;
        }

        public override bool BlackMarket
        {
            get { return m_commodity.BlackMarket(); }
        }

        public OperationStatus<int> Buy(int quantity)
        {
            if (AssignedShip == null)
            {
                throw new InvalidOperationException("Opportunity can only be used with a ship");
            }

            if (AssignedShip.System.Type != Source || AssignedShip.Location != SpaceShipLocation.Planet)
            {
                return new OperationStatus<int>(0, "Spaceship must be present on the planet of the source system");
            }

            int bought = AssignedShip.Player.BuyCommodity(Game.StarSystems[Source], m_commodity, m_unitCost, quantity);
            bought = AssignedShip.Player.MoveBoughtCommodity(Game.StarSystems[Source], AssignedShip, m_commodity, bought);
            return bought;
        }

        public int Sell(BlackMarket blackMarket)
        {
            if (AssignedShip == null)
            {
                throw new InvalidOperationException("Opportunity can only be used with a ship");
            }

            if (AssignedShip.System.Type != Destination || AssignedShip.Location != SpaceShipLocation.Planet)
            {
                throw new InvalidOperationException("Spaceship must be present on the planet of the source system");
            }

            int price = blackMarket.CalculatePrice(m_commodity);
            int sold = AssignedShip.Player.SellCommodity(AssignedShip, m_commodity, price, AssignedShip.GetCount(m_commodity));
            Debug.Assert(AssignedShip.GetCount(m_commodity) == 0);

            // the opportunity is used-up after the sale
            Deactivate();
            return sold;
        }
    }
}