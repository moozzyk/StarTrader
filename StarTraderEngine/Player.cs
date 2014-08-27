namespace StarTrader
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Player
    {
        private readonly string m_name;
        private readonly List<Spaceship> m_ships = new List<Spaceship>();
        private readonly Reputation m_reputation;
        private readonly Dictionary<StarSystem, CommodityStorage> m_temporaryStorage = new Dictionary<StarSystem, CommodityStorage>();

        public Player(string name, int cash, int reputation, int political, int economic, int criminal)
        {
            m_name = name;
            Cash = cash;
            m_reputation = new Reputation(reputation, political, economic, criminal);
        }

        public string Name
        {
            get { return m_name; }
        }

        public int Cash { get; set; }

        public int Initiative { get; set; }

        public int Order { get; set; }

        public Reputation Reputation
        {
            get { return m_reputation; }
        }

        public int GetInitiative(int cost)
        {
            if (cost > Cash)
            {
                throw new InvalidOperationException("Not enough cash to buy initiative");
            }

            Cash -= cost;
            return cost + Dice.Roll();
        }

        public int MoveBoughtCommodity(StarSystem source, CommodityStorage destination, Commodity commodity, int quantity)
        {
            if (!m_temporaryStorage.ContainsKey(source))
            {
                return 0;
            }

            return m_temporaryStorage[source].MoveTo(destination, commodity, quantity);
        }

        /// <summary>
        /// Attempts to buy requested amount of storage space
        /// Returns the actual amount of storage bought
        /// </summary>
        public int BuyWarehouse(StarSystem system, int capacity)
        {
            var warehouse = system.GetWarehouse(this);
            int actualCapacity = Math.Min(capacity, Cash / Warehouse.Price);
            warehouse.Size += actualCapacity;
            Cash -= actualCapacity * Warehouse.Price;
            return actualCapacity;
        }

        /// <summary>
        /// Bought commodity is placed in available space (starting with warehouses)
        /// Players can move the marchandise later
        /// Returns the amount actually bought
        /// </summary>
        public int BuyCommodity(StarSystem system, Commodity commodity, int price, int quantity)
        {
            // calculate how much we can buy
            int canBuy = Math.Min(Cash / price, quantity);
            Debug.Assert(canBuy >= 0);

            if (canBuy <= 0)
            {
                return 0;
            }

            // TODO - get actual amount here
            // canBuy = from ui;
            if (canBuy <= 0)
            {
                // the player has to buy at least 1
                canBuy = 1;
            }

            Cash -= canBuy * price;

            // store the commodity in temp storage
            MoveToTempStorage(system, commodity, canBuy);
            return canBuy;
        }

        private void MoveToTempStorage(StarSystem system, Commodity commodity, int canBuy)
        {
            if (!m_temporaryStorage.ContainsKey(system))
            {
                // no limits on temporary storage
                m_temporaryStorage[system] = new InfiniteStorage();
            }

            m_temporaryStorage[system].Store(commodity, canBuy);
        }

        /// <summary>
        /// Returns the amount actually sold
        /// </summary>
        public int SellCommodity(StarSystem system, Commodity commodity, int price, int quantity)
        {
            // calculate how much we can sell - check warehouses, ships in port
            int available = system.GetWarehouse(this).GetCount(commodity) +
                m_ships.Where(s => s.System.Equals(system) && s.Location == SpaceShipLocation.Port).Sum(s => s.GetCount(commodity));

            // buying and seeling on the same market is not allowed, so temp storage should be empty
            Debug.Assert(!m_temporaryStorage.ContainsKey(system) || m_temporaryStorage[system].GetCount(commodity) == 0);
            int canSell = Math.Min(quantity, available);

            // store the commodity
            int sold = SellCommodity(system.GetWarehouse(this), commodity, price, canSell);

            foreach (var ship in m_ships.TakeWhile(s => sold < canSell))
            {
                sold += SellCommodity(ship, commodity, price, canSell - sold);
            }

            Debug.Assert(sold == canSell);
            return sold;
        }

        /// <summary>
        /// Returns the amount actually sold
        /// </summary>
        public int SellCommodity(CommodityStorage storage, Commodity commodity, int price, int quantity)
        {
            // calculate how much we can sell
            int available = storage.GetCount(commodity);

            int canSell = Math.Min(quantity, available);

            // store the commodity
            int removed = storage.Remove(commodity, canSell);
            Cash += removed * price;

            Debug.Assert(removed <= canSell);
            return removed;
        }

        public OperationStatus<Spaceship> BuyShip(HullType hullType, StarSystem destination, SpaceShipLocation location)
        {
            HullAttribute hull = HullAttribute.GetAttibute(hullType);
            if (hull.Price > Cash)
            {
                return new OperationStatus<Spaceship>(null, string.Format("Insufficient cash (required {0}, available {1})", hull.Price, Cash));
            }

            // when buying we ignore the (in)ability to land the ship on the planet
            var ship = new Spaceship(this, hullType, CrewClass.D, destination, location);
            Cash -= hull.Price;
            m_ships.Add(ship);
            return ship;
        }

        public void RemoveShip(Spaceship ship)
        {
            Debug.Assert(Equals(ship.Player));
            Debug.Assert(m_ships.Contains(ship));
            m_ships.Remove(ship);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Player;
            return other != null && Name.Equals(other.Name);
        }

        public override string ToString()
        {
            return "Player: " + Name;
        }
    }
}
