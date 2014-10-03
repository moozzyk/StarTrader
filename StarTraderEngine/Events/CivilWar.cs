namespace StarTrader.Events
{
    using System.Linq;

    /// <summary>
    /// Civil war in Gamma Leporis. Weapons prices sold on the Gamma Leporis planet increase 3x. 
    /// All ships in that space port and all factories and warehouses on Gamma Leporis are nationalized - discard them. 
    /// Owners are compensated at 50% current value of the factories, warehouse prices and catalog price of the ships (hull and modules, not the crew). 
    /// Commodities are lost.
    /// </summary>
    class CivilWar : GameEvent
    {
        public const int WeaponsPriceMultiplier = 3;
        private const int CompensationRatio = 2;

        public CivilWar(Game game, int delay, Connections requiredConnections, bool reusable) :
            base(game, delay, requiredConnections, reusable, "Civil war")
        {
        }

        protected override void Execute()
        {
            base.Execute();
            StarSystem gammaLeporis = Game.StarSystems[StarSystemType.GammaLeporis];
            gammaLeporis.AtWar = true;
            foreach (var player in Game.Players)
            {
                foreach (var ship in player.Ships.Where(s => s.System.Equals(gammaLeporis)).ToList())
                {
                    player.RemoveShip(ship);
                    player.Cash += ship.GetPrice() / CompensationRatio;
                }

                foreach (var f in gammaLeporis.GetFactories(player))
                {
                    player.Cash += f.GetPrice(gammaLeporis) / CompensationRatio;
                }

                gammaLeporis.GetFactories(player).Clear();

                gammaLeporis.GetWarehouse(player).Empty();
                player.Cash += gammaLeporis.GetWarehouse(player).GetPrice() / CompensationRatio;
            }
        }

        protected override void Reset()
        {
            base.Reset();
            Game.StarSystems[StarSystemType.GammaLeporis].AtWar = false;
        }
    }
}