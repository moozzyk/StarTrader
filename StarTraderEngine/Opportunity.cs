using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace StarTrader
{
    public class Opportunity : GameEvent
    {
        private readonly Commodity m_commodity;
        private readonly int m_unitCost;
        private readonly StarSystemType m_source;

        private Spaceship m_assignedShip;

        public Opportunity(int delay, Connections requiredConnections, bool reusable, string description,
            Commodity commodity, int unitCost, StarSystemType source)
            : base(delay, requiredConnections, reusable, description)
        {
            m_commodity = commodity;
            m_unitCost = unitCost;
            m_source = source;
        }

        public StarSystemType? Destination { get; set; }

        public SpaceShipLocation? Location { get; set; }

        public int SellPrice { get; set; }

        public int Limit { get; set; }

        public virtual OperationStatus<bool> Assign(Game game, Spaceship ship)
        {
            if (m_commodity.BlackMarket() && ship.Location != SpaceShipLocation.Planet)
            {
                // TODO - should this throw
                return new OperationStatus<bool>(false, "A black market opportunity can only be used on the planet surface");
            }

            if (!IsKnownTo(ship.Player))
            {
                return new OperationStatus<bool>(false, "This opportunity has not been revealed to {0}" + ship.Player);
            }

            ship.Add(this);

            Debug.Assert(m_assignedShip == null);
            m_assignedShip = ship;

            game.CurrentEvents.Remove(this);
            return true;
        }

        protected override void Reset()
        {
            base.Reset();
            if (m_assignedShip != null)
            {
                m_assignedShip.Remove(this);
                m_assignedShip = null;
            }
        }

        public OperationStatus<int> BuyCommodity(Game game, int quantity)
        {
            if (m_assignedShip == null)
            {
                throw new InvalidOperationException("Opportunity can only be used with a ship");
            }

            if (m_assignedShip.System.Type != m_source || m_assignedShip.Location != SpaceShipLocation.Planet)
            {
                return new OperationStatus<int>(0, "Spaceship must be present on the planet of the source system");
            }

            int bought = m_assignedShip.Player.BuyCommodity(game.StarSystems[m_source], m_commodity, m_unitCost, quantity);
            bought = m_assignedShip.Player.MoveBoughtCommodity(game.StarSystems[m_source], m_assignedShip, m_commodity, bought);
            return bought;
        }

        public int SellCommodity(Game game, BlackMarket blackMarket)
        {
            if (m_assignedShip == null)
            {
                throw new InvalidOperationException("Opportunity can only be used with a ship");
            }

            if (m_assignedShip.System.Type != Destination || m_assignedShip.Location != SpaceShipLocation.Planet)
            {
                throw new InvalidOperationException("Spaceship must be present on the planet of the source system");
            }

            int price = blackMarket.CalculatePrice(m_commodity);
            int sold = m_assignedShip.Player.SellCommodity(m_assignedShip, m_commodity, price, m_assignedShip.GetCount(m_commodity));
            Debug.Assert(m_assignedShip.GetCount(m_commodity) == 0);

            // the opportunity is used-up after the sale
            Deactivate(game);
            return sold;
        }

        public class Module : Opportunity, IEnumerable<ShipModuleType>
        {
            private readonly List<ShipModuleType> m_allowedModules = new List<ShipModuleType>();

            public Module(int delay, Connections requiredConnections, bool reusable, string description, StarSystemType source)
                : base(delay, requiredConnections, reusable, description, Commodity.Module, 0, source)
            {
            }

            public IEnumerator<ShipModuleType> GetEnumerator()
            {
                return m_allowedModules.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Add(ShipModuleType shipModule)
            {
                m_allowedModules.Add(shipModule);
            }
        }

        public class Hull : Opportunity
        {
            private readonly HullType? m_hullType;
            private readonly int m_allowedModules;

            public Hull(int delay, Connections requiredConnections, bool reusable, string description, StarSystemType source, HullType? hullType, int allowedModules)
                : base(delay, requiredConnections, reusable, description, Commodity.Module /*irrelevant*/, 0, source)
            {
                // null means any black-market
                m_hullType = hullType;
                m_allowedModules = allowedModules;
            }
        }

        public class EnvoyTransport : Opportunity
        {
            private readonly int m_requiredReputation;

            public EnvoyTransport(int delay, Connections requiredConnections, bool reusable, string description, StarSystemType source, int requiredReputation)
                : base(delay, requiredConnections, reusable, description, Commodity.Passengers, 0, source)
            {
                m_requiredReputation = requiredReputation;
            }
        }

        public class Delivery : Opportunity
        {
            public Delivery(int delay, Connections requiredConnections, bool reusable, string description, StarSystemType source)
                : base(delay, requiredConnections, reusable, description, Commodity.Food /*irrelevant*/, 0, source)
            {
            }

            public int ReputationBoost { get; set; }

            public int RequiredFreight { get; set; }

            public int RequiredPassenger { get; set; }
        }

        public class ResearchExpedition : Opportunity
        {
            public ResearchExpedition(int delay, Connections requiredConnections, bool reusable, string description)
                : base(delay, requiredConnections, reusable, description, Commodity.Furs /*irrelevant*/, 0, StarSystemType.BetaHydri /*irrelevant*/)
            {
                // Organized by independent corporation. Player can send 1 legal ship. 
                // Roll 1D and move the ship to the turn ahead by the number rolled - that's the turn when the ship returns. if rolled 1, the ship is destroyed.
                // After the return roll 2D and multiple by 50 for the reward
            }
        }
    }
}
