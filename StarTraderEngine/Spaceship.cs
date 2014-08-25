namespace StarTrader
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public enum SpaceShipLocation
    {
        Port,
        Hangar,
        Shipyard,
        Space,
        Planet
    }

    public class Spaceship : CommodityStorage, IEnumerable<ShipModule>
    {
        private readonly HullAttribute m_hull;
        private readonly List<ShipModule> m_modules = new List<ShipModule>();
        private readonly List<Opportunity> m_opportunities = new List<Opportunity>();

        public Spaceship(Player player, HullType hull, CrewClass crew, StarSystem system, SpaceShipLocation location)
        {
            Player = player;
            System = system;
            Location = location;
            m_hull = HullAttribute.GetAttibute(hull);
            SetCrew(crew);
            Size = m_hull.Capacity;
        }

        public StarSystem System { get; private set; }

        public Player Player { get; private set; }

        public int AvailableModuleCapacity
        {
            get { return m_hull.ModuleCapacity - m_modules.Sum(m => m.RequiredCapacity); }
        }

        override public int AvailableCapacity
        {
            get { return base.AvailableCapacity + m_modules.Sum(m => m.AvailableCapacity); }
        }

        protected override int StoragePerUnit
        {
            get { return 2; }
        }

        public SpaceShipLocation Location { get; private set; }

        public CrewAttribute Crew { get; private set; }

        public void SetCrew(CrewClass crew)
        {
            Crew = CrewAttribute.GetAttibute(crew);
        }

        public void SetLocation(SpaceShipLocation location)
        {
            if (location == SpaceShipLocation.Planet && !m_hull.Aerodynamic)
            {
                throw new InvalidOperationException("Can't land on the planet");
            }

            Location = location;
        }

        public int GetJumpSuccessModifier()
        {
            int modifier = Crew.SafeJumpModifier + m_modules.Sum(m => m.SafeJumpModifier);
            return modifier;
        }

        public override int GetCount(Commodity commodity)
        {
            return base.GetCount(commodity) + m_modules.Sum(m => m.GetCount(commodity));
        }

        public override int Remove(Commodity commodity, int quantity)
        {
            int actuallyRemoved = base.Remove(commodity, quantity);
            foreach (var module in m_modules.TakeWhile(module => actuallyRemoved < quantity))
            {
                actuallyRemoved += module.Remove(commodity, quantity - actuallyRemoved);
            }

            return actuallyRemoved;
        }

        public override int Store(Commodity commodity, int quantity)
        {
            int actuallyStored = base.Store(commodity, quantity);
            foreach (var module in m_modules.TakeWhile(module => actuallyStored < quantity))
            {
                actuallyStored += module.Store(commodity, quantity - actuallyStored);
            }

            return actuallyStored;
        }

        public IEnumerator<ShipModule> GetEnumerator()
        {
            return m_modules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ShipModule>)this).GetEnumerator();
        }

        public void Add(ShipModule module)
        {
            if (AvailableModuleCapacity >= module.RequiredCapacity)
            {
                m_modules.Add(module);
            }
            else
            {
                throw new InvalidOperationException("Ship doesn't have required capacity to add a new module");
            }
        }

        public void Add(Opportunity opportunity)
        {
            m_opportunities.Add(opportunity);
        }

        public void Remove(Opportunity opportunity)
        {
            m_opportunities.Remove(opportunity);
        }

        public OperationStatus<bool> BuyModule(ShipModuleType module)
        {
            ModuleAttribute moduleAttribute = ModuleAttribute.GetAttibute(module);
            if (AvailableModuleCapacity < moduleAttribute.RequiredCapacity)
            {
                return new OperationStatus<bool>(false,
                    string.Format("Spaceship doesn't have required module capacity (required {0}, available {1})",
                        moduleAttribute.RequiredCapacity, AvailableModuleCapacity));
            }

            if (moduleAttribute.Price > Player.Cash)
            {
                return new OperationStatus<bool>(false,
                    string.Format("Not enough cash (required {0}, available {1})", moduleAttribute.Price,
                        Player.Cash));
            }

            Add(new ShipModule(module));
            Player.Cash -= moduleAttribute.Price;
            return true;
        }
    }
}
