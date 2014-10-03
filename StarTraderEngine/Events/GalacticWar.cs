namespace StarTrader.Events
{
    internal class GalacticWar : PriceModifier
    {
        private const int ShipTradeModifier = 5;
        private const int PoliceEfficiencyModifier = 4;

        /// <summary>
        /// Component prices +3. Polymers +6. Isotopes +2. During this turn (and next) ships cannot be bought (including black market). Ignore applicable events. 
        /// Add 5 to the die roll for spaceship sellers during these stages. Increment Police Efficiency by 4 during this turn.
        /// </summary>
        public GalacticWar(Game game, int delay, Connections requiredConnections, bool reusable) :
            base(game, delay, requiredConnections, reusable, "Galactic war", PriceModifier, null)
        {
        }

        protected override void Execute()
        {
            base.Execute();
            Game.ShipTradeAllowed = false;
            Game.ShipTradeModifier += ShipTradeModifier;
            foreach (var system in Game.StarSystems.Values)
            {
                system.PoliceEfficiency += PoliceEfficiencyModifier;
            }
        }

        protected override void Reset()
        {
            base.Reset();
            Game.ShipTradeAllowed = true;
            Game.ShipTradeModifier -= ShipTradeModifier;
            foreach (var system in Game.StarSystems.Values)
            {
                system.PoliceEfficiency -= PoliceEfficiencyModifier;
            }
        }

        private static int PriceModifier(Commodity commodity)
        {
            switch (commodity)
            {
                case Commodity.Component:
                    return 3;
                case Commodity.Polymer:
                    return 6;
                case Commodity.Isotope:
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
