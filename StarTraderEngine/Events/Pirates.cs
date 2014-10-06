namespace StarTrader.Events
{
    using System.Linq;

    class Pirates : GameEvent
    {
        private const int SecurityModifier = 2;

        /// <summary>
        /// On Mu Herkulis. All commodities and modules in warehouses are lost. Roll 1D for each ship in space port in Mu Herkulis. 
        /// If <= 3 - the ship and everything onboard is lost, >=4 ship escapes into inter-planetary space. Police efficiency and Security level go up by 2 during this turn.
        /// </summary>
        public Pirates(Game game, int delay, Connections requiredConnections, bool reusable) : base(game, delay, requiredConnections, reusable, "Pirate attack")
        {
        }

        protected override void Execute()
        {
            base.Execute();
            StarSystem muHerculis = Game.StarSystems[StarSystemType.MuHerculis];
            muHerculis.PoliceEfficiency += SecurityModifier;
            muHerculis.SecurityLevel += SecurityModifier;
            foreach (var player in Game.Players)
            {
                muHerculis.GetFactories(player).Clear();
                muHerculis.GetWarehouse(player).Empty();

                int roll = Dice.Roll1Die();
                foreach (var ship in player.Ships.Where(s => s.System.Equals(muHerculis) && s.Location == SpaceShipLocation.Port).ToList())
                {
                    if (roll < 4)
                    {
                        player.RemoveShip(ship);
                    }
                    else
                    {
                        ship.SetLocation(SpaceShipLocation.Space);
                    }
                }
            }
        }

        protected override void Reset()
        {
            base.Reset();
            StarSystem muHerculis = Game.StarSystems[StarSystemType.MuHerculis];
            muHerculis.PoliceEfficiency -= SecurityModifier;
            muHerculis.SecurityLevel -= SecurityModifier;
        }
    }
}