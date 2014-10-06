namespace StarTrader.Events
{
    using System;
    using System.Linq;

    class Tax : GameEvent
    {
        private const int FactoryTax = 2;
        private const int WarehouseTax = 1;
        private const int SpaceshipTax = 5;

        /// <summary>
        /// Every player immediately pays 1 HT for each warehouse capacity unit, 2 HT for each unit of factory capacity and 5 HT for each space ship.
        /// Up to available cash.
        /// </summary>
        public Tax(Game game, int delay, Connections requiredConnections, bool reusable)
            : base(game, delay, requiredConnections, reusable, "Special tax")
        {
        }

        protected override void Execute()
        {
            base.Execute();
            foreach (var player in Game.PlayersByInitiative)
            {

                if (player.Cash <= 0)
                {
                    continue;
                }

                int tax = 0;
                foreach (var starSystem in Game.StarSystems.Values)
                {
                    var warehouse = starSystem.GetWarehouse(player);
                    tax += warehouse.Size * WarehouseTax;
                    tax += starSystem.GetFactories(player).Sum(factory => factory.Capacity * FactoryTax);
                }

                tax += player.Ships.Sum(ship => SpaceshipTax);
                player.Cash = Math.Min(0, player.Cash - tax);
            }
        }
    }
}