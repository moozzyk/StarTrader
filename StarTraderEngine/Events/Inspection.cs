namespace StarTrader.Events
{
    class Inspection : GameEvent
    {
        public Inspection(Game game, int delay, Connections requiredConnections, bool reusable) : base(game, delay, requiredConnections, reusable, "Control")
        {
        }

        protected override void Execute()
        {
            base.Execute();
            foreach (var player in Game.PlayersByInitiative)
            {
                int roll = Dice.Roll();
                player.Reputation.AdjustReputation(-roll);
            }
        }
    }
}