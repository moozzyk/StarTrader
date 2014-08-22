using System.Collections;
using System.Collections.Generic;

namespace StarTrader
{
    public class Opportunity : GameEvent
    {
        private readonly Commodity m_commodity;
        private readonly int m_unitCost;
        private readonly StarSystemType m_source;

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

        public virtual OperationStatus Assign(Spaceship ship)
        {
            if (m_commodity.BlackMarket() && ship.Location != SpaceShipLocation.Planet)
            {
                return new OperationStatus(false, "A black market opportunity can only be used on the planet surface");
            }

            if (!IsKnownTo(ship.Player))
            {
                return new OperationStatus(false, "This opportunity has not been revealed to {0}" + ship.Player);
            }

            // TODO - assign it to the ship
            return true;
        }

        protected override void Reset()
        {
            base.Reset();
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

            public Hull(int delay, Connections requiredConnections, bool reusable, string description, StarSystemType source, HullType? hullType, int allowedModules)
                : base(delay, requiredConnections, reusable, description, Commodity.Module /*irrelevant*/, 0, source)
            {
                // null means any black-market
                m_hullType = hullType;
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
