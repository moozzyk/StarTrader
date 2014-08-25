using System;
using System.Collections.Generic;

namespace StarTrader
{
    public class HullOpportunity : Opportunity
    {
        private readonly HullType? m_hullType;
        private readonly int m_allowedModules;
        private bool m_hullBought;
        private int m_boughtModules;

        public HullOpportunity(Game game, int delay, Connections requiredConnections, bool reusable, string description, StarSystemType source, HullType? hullType, int allowedModules)
            : base(game, delay, requiredConnections, reusable, description, source)
        {
            // null means any black-market
            m_hullType = hullType;
            m_allowedModules = allowedModules;
        }

        public override bool BlackMarket
        {
            get { return m_hullType == null || HullAttribute.GetAttibute(m_hullType.Value).Military; }
        }

        protected override void Reset()
        {
            base.Reset();
            m_hullBought = false;
            m_boughtModules = 0;
        }

        public OperationStatus<bool> Buy(HullType hullType)
        {
            if (AssignedShip == null)
            {
                throw new InvalidOperationException("Opportunity can only be used with a ship");
            }

            if (m_hullBought)
            {
                throw new InvalidOperationException("Ship can only be bought once");
            }

            if (m_hullType != null && m_hullType != hullType)
            {
                throw new InvalidOperationException("Hull type not allowed");
            }

            var result = AssignedShip.Player.BuyShip(hullType, Game.StarSystems[Source], SpaceShipLocation.Planet);
            var ship = (Spaceship)result;
            if (ship == null)
            {
                return new OperationStatus<bool>(false, result.Detail);
            }

            m_hullBought = true;
            return true;
        }

        public OperationStatus<bool> BuyModules(Spaceship ship, ShipModuleType type)
        {
            if (ship.System.Type != Source)
            {
                return new OperationStatus<bool>(false, "Spaceship must be present on the planet of the source system");
            }

            if (!ModuleAttribute.GetAttibute(type).Military)
            {
                return new OperationStatus<bool>(false, "Only black market (military) modules are available");
            }

            if (m_boughtModules == m_allowedModules)
            {
                return new OperationStatus<bool>(false, "Maximum number of modules allowed reached");
            }

            var result = ship.BuyModule(type);
            if (result)
            {
                m_boughtModules++;
            }

            if (m_boughtModules == m_allowedModules)
            {
                Deactivate();
            }

            return result;
        }
    }
}