namespace StarTrader
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class GameEvent
    {
        private const int Price = 5;

        private readonly int m_delay;
        private readonly Connections m_requiredConnections;
        private readonly bool m_reusable;
        private readonly string m_description;
        private readonly HashSet<Player> m_knownTo = new HashSet<Player>();

        private int m_turnToActivate;

        public GameEvent(int delay, Connections requiredConnections, bool reusable, string description)
        {
            m_delay = delay;
            m_requiredConnections = requiredConnections;
            m_reusable = reusable;
            m_description = description;
        }

        public Connections RequiredConnections
        {
            get { return m_requiredConnections; }
        }

        public string Description
        {
            get { return m_description; }
        }

        public bool Deactivate(Game game)
        {
            if (m_turnToActivate < game.Turn)
            {
                Debug.Assert(game.CurrentEvents.Contains(this));
                Debug.Assert(!game.AvailableEvents.Contains(this));
                game.CurrentEvents.Remove(this);
                if (m_reusable)
                {
                    game.AvailableEvents.Add(this);
                }

                Reset();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Called during the event phase
        /// </summary>
        public bool Activate(Game game)
        {
            if (m_turnToActivate == game.Turn)
            {
                // do stuff
                return true;
            }

            return false;
        }

        /// <summary>
        /// Invoked when the event is drawn from the pool
        /// </summary>
        public void Draw(Game game)
        {
            Debug.Assert(m_turnToActivate == 0);
            Debug.Assert(!game.CurrentEvents.Contains(this));
            Debug.Assert(game.AvailableEvents.Contains(this));
            Debug.Assert(m_knownTo.Count == 0);
            m_turnToActivate = game.Turn + m_delay;
            game.CurrentEvents.Add(this);
            game.AvailableEvents.Remove(this);
        }

        public bool Reveal(Player player)
        {
            if (IsKnownTo(player))
            {
                return true;
            }

            if (player.Cash < Price)
            {
                return false;
            }

            if (!player.Reputation.MeetsRequirements(RequiredConnections))
            {
                return false;
            }

            m_knownTo.Add(player);
            player.Cash -= Price;

            return true;
        }

        public bool IsKnownTo(Player player)
        {
            Debug.Assert(m_turnToActivate > 0);
            return m_knownTo.Contains(player);
        }

        protected virtual void Reset()
        {
            m_turnToActivate = 0;
            m_knownTo.Clear();
        }
    }
}
