using System;
using System.Linq;

namespace StarTrader
{
    public class DeliveryOpportunity : Opportunity
    {
        private bool m_cargoLoaded;

        public DeliveryOpportunity(Game game, int delay, Connections requiredConnections, bool reusable, string description, StarSystemType source)
            : base(game, delay, requiredConnections, reusable, description, source)
        {
        }

        public int ReputationBoost { get; set; }

        public int RequiredFreight { get; set; }

        public int RequiredPassenger { get; set; }

        public override bool BlackMarket
        {
            get { return false; }
        }

        public override OperationStatus<bool> Assign(Spaceship ship)
        {
            if (ship.Count(m => m.Size > 0) < RequiredFreight || ship.Count(m => m.LifeSupport) < RequiredPassenger)
            {
                return new OperationStatus<bool>(false, "Delivery requires a freight and/or passenger modules");
            }

            return base.Assign(ship);
        }

        public OperationStatus<bool> Load()
        {
            if (AssignedShip.System.Type != Source || AssignedShip.Location != Location)
            {
                return new OperationStatus<bool>(false, "Spaceship must be present in the requied location of the source system");
            }

            m_cargoLoaded = true;
            return true;
        }

        public OperationStatus<bool> Deliver()
        {
            if (!m_cargoLoaded)
            {
                throw new InvalidOperationException("Cargo must be loaded before delivery");
            }

            if (AssignedShip.System.Type != Destination || AssignedShip.Location != Location)
            {
                return new OperationStatus<bool>(false, "Spaceship must be present in the requied location of the destination system");
            }

            AssignedShip.Player.Cash += SellPrice;
            AssignedShip.Player.Reputation.AdjustReputation(ReputationBoost);
            Deactivate();
            return true;
        }

        protected override void Reset()
        {
            base.Reset();
            m_cargoLoaded = false;
        }
    }
}