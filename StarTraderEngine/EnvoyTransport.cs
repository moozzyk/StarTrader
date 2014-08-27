using System;
using System.Linq;

namespace StarTrader
{
    public class EnvoyTransport : Opportunity
    {
        private readonly int m_requiredReputation;
        private bool m_envoyBoarded;

        public EnvoyTransport(Game game, int delay, Connections requiredConnections, bool reusable, string description, StarSystemType source, int requiredReputation)
            : base(game, delay, requiredConnections, reusable, description, source)
        {
            m_requiredReputation = requiredReputation;
        }

        public override bool BlackMarket
        {
            get { return false; }
        }

        public override OperationStatus<bool> Assign(Spaceship ship)
        {
            if (ship.Player.Reputation.Current < m_requiredReputation)
            {
                return new OperationStatus<bool>(false, string.Format("Insufficient reputation (required {0})", m_requiredReputation));
            }

            if (!ship.Any(m => m.LifeSupport))
            {
                return new OperationStatus<bool>(false, "Envoy transport requires a passenger module");
            }

            return base.Assign(ship);
        }

        public OperationStatus<bool> Embark()
        {
            if (AssignedShip.System.Type != Source || AssignedShip.Location != Location)
            {
                return new OperationStatus<bool>(false, "Spaceship must be present in the requied location of the source system");
            }

            m_envoyBoarded = true;
            return true;
        }

        public OperationStatus<bool> Deliver()
        {
            if (!m_envoyBoarded)
            {
                throw new InvalidOperationException("Envoy must board before delivery");
            }

            if (AssignedShip.System.Type != Destination || AssignedShip.Location != Location)
            {
                return new OperationStatus<bool>(false, "Spaceship must be present in the requied location of the destination system");
            }

            AssignedShip.Player.Cash += SellPrice;
            Deactivate();
            return true;
        }

        protected override void Reset()
        {
            base.Reset();
            m_envoyBoarded = false;
        }
    }
}