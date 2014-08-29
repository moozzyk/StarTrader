namespace StarTrader.Events
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class ModuleOpportunity : Opportunity, IEnumerable<ShipModuleType>
    {
        private readonly List<ShipModuleType> m_allowedModules = new List<ShipModuleType>();

        public ModuleOpportunity(Game game, int delay, Connections requiredConnections, bool reusable, string description, StarSystemType source)
            : base(game, delay, requiredConnections, reusable, description, source)
        {
        }

        public override bool BlackMarket
        {
            get { return m_allowedModules.Any(m => ModuleAttribute.GetAttibute(m).Military); }
        }

        public OperationStatus<bool> Buy(ShipModuleType module)
        {
            if (AssignedShip == null)
            {
                throw new InvalidOperationException("Opportunity can only be used with a ship");
            }

            if (m_allowedModules.All(m => m != module))
            {
                throw new InvalidOperationException("Module type not allowed");
            }

            if (AssignedShip.System.Type != Source || AssignedShip.Location != SpaceShipLocation.Planet)
            {
                return new OperationStatus<bool>(false, "Spaceship must be present on the planet of the source system");
            }

            var result = AssignedShip.BuyModule(module);

            // the opportunity is used-up after the sale
            if (result)
            {
                Deactivate();
            }

            return result;
        }

        public IEnumerator<ShipModuleType> GetEnumerator()
        {
            return m_allowedModules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ShipModuleType shipModule)
        {
            m_allowedModules.Add(shipModule);
        }
    }
}