namespace StarTrader.Events
{
    using System;

    public class ResearchExpedition : Opportunity
    {
        private const int RewardMultiplier = 50;
        private int m_timeOfReturn;

        public ResearchExpedition(Game game, int delay, Connections requiredConnections, bool reusable, string description)
            : base(game, delay, requiredConnections, reusable, description, StarSystemType.BetaHydri /*irrelevant*/)
        {
            // Organized by independent corporation. Player can send 1 legal ship. 
            // Roll 1D and move the ship to the turn ahead by the number rolled - that's the turn when the ship returns. if rolled 1, the ship is destroyed.
            // After the return roll 2D and multiple by 50 for the reward
        }

        public override bool BlackMarket
        {
            get { return false; }
        }

        public override OperationStatus<bool> Assign(Spaceship ship)
        {
            if (ship.Illegal)
            {
                return new OperationStatus<bool>(false, "Research expedition requires a legal ship");
            }

            return base.Assign(ship);
        }

        public OperationStatus<bool> Begin()
        {
            int turnsRemaining = Game.Dice.Roll1Die();
            if (turnsRemaining == 1)
            {
                AssignedShip.Player.RemoveShip(AssignedShip);
                Deactivate();
                return new OperationStatus<bool>(false, "Ship has been destroyed during the expedition");
            }

            m_timeOfReturn = Game.Turn + turnsRemaining;
            return true;
        }

        public OperationStatus<bool> Return()
        {
            if (m_timeOfReturn == 0)
            {
                throw new InvalidOperationException("Expedition needs to be begin before it returns");
            }

            if (Game.Turn < m_timeOfReturn)
            {
                return new OperationStatus<bool>(false, string.Format("Expedition will complete in turn {0}", m_timeOfReturn));
            }

            AssignedShip.Player.Cash += Game.Dice.Roll() * RewardMultiplier;
            Deactivate();
            return true;
        }

        protected override void Reset()
        {
            base.Reset();
            m_timeOfReturn = 0;
        }
    }
}