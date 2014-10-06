namespace StarTrader.Events
{
    class Epidemic : PriceModifier
    {
        private const int PriceModifier = 4;
        private const int SecurityModifier = 1;

        public Epidemic(Game game, int delay, Connections requiredConnections, bool reusable)
            : base(game, delay, requiredConnections, reusable, "Epidemic", c => PriceModifier, s => true)
        {
        }

        protected override void Execute()
        {
            base.Execute();
            Game.HyperjumpAllowed = location => location != SpaceShipLocation.Port;
            foreach (var system in Game.StarSystems.Values)
            {
                system.PoliceEfficiency += SecurityModifier;
                system.SecurityLevel += SecurityModifier;
            }
        }

        protected override void Reset()
        {
            base.Reset();
            Game.HyperjumpAllowed = null;
            foreach (var system in Game.StarSystems.Values)
            {
                system.PoliceEfficiency -= SecurityModifier;
                system.SecurityLevel -= SecurityModifier;
            }
        }
    }
}