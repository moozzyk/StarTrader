namespace StarTrader.Events
{
    class Inflation : GameEvent
    {
        /// <summary>
        /// Fake bank notes flood the market. Cut everyone's cash by 50% (round up). Also cut all unpaid loans (don't change interest).
        /// </summary>
        public Inflation(Game game, int delay, Connections requiredConnections, bool reusable) : 
            base(game, delay, requiredConnections, reusable, "Inflation")
        {
        }

        protected override void Execute()
        {
            base.Execute();
            foreach (var player in Game.Players)
            {
                player.Cash /= 2;
                player.Debt /= 2;
            }
        }
    }
}