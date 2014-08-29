namespace StarTrader.Events
{
    internal class HyperjumpModifier : GameEvent
    {
        private readonly int m_hyperjumpModifier;

        public HyperjumpModifier(Game game, int delay, Connections requiredConnections, bool reusable, string description, int hyperjumpModifier)
            : base(game, delay, requiredConnections, reusable, description)
        {
            m_hyperjumpModifier = hyperjumpModifier;
        }

        protected override void Execute()
        {
            base.Execute();
            Game.HyperjumpModifier = m_hyperjumpModifier;
        }

        protected override void Reset()
        {
            base.Reset();
            Game.HyperjumpModifier = 0;
        }
    }
}