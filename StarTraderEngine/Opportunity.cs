using System.Diagnostics;

namespace StarTrader
{
    public abstract class Opportunity : GameEvent
    {
        private readonly StarSystemType m_source;
        private Spaceship m_assignedShip;

        protected Opportunity(Game game, int delay, Connections requiredConnections, bool reusable, string description, StarSystemType source)
            : base(game, delay, requiredConnections, reusable, description)
        {
            m_source = source;
        }

        public StarSystemType? Destination { get; set; }

        public SpaceShipLocation? Location { get; set; }

        public int SellPrice { get; set; }

        public int Limit { get; set; }

        public abstract bool BlackMarket { get; }

        protected Spaceship AssignedShip
        {
            get { return m_assignedShip; }
        }

        public StarSystemType Source
        {
            get { return m_source; }
        }

        public virtual OperationStatus<bool> Assign(Spaceship ship)
        {
            if (BlackMarket && ship.Location != SpaceShipLocation.Planet)
            {
                // TODO - should this throw
                return new OperationStatus<bool>(false, "A black market opportunity can only be used on the planet surface");
            }

            if (!IsKnownTo(ship.Player))
            {
                return new OperationStatus<bool>(false, "This opportunity has not been revealed to {0}" + ship.Player);
            }

            ship.Add(this);

            Debug.Assert(AssignedShip == null);
            m_assignedShip = ship;

            Game.CurrentEvents.Remove(this);
            return true;
        }

        protected override void Reset()
        {
            base.Reset();
            if (AssignedShip != null)
            {
                AssignedShip.Remove(this);
                m_assignedShip = null;
            }
        }
    }
}
