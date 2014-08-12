﻿namespace StarTrader
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

		public Player(string name, int political, int economic, int criminal)
		{
			m_name = name;
			Cash = Game.Scenario.StartingCash;
			m_reputation = new Reputation(political, economic, criminal);
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
			int actualCapacity = Math.Min(capacity, Cash / (capacity * Warehouse.Price));
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
				m_temporaryStorage[system] = new CommodityStorage { Size = int.MaxValue };
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

			Cash += canSell * price;

			// store the commodity
			int removed = system.GetWarehouse(this).Remove(commodity, canSell);

			foreach (var ship in m_ships.TakeWhile(s => removed < canSell))
			{
				removed += ship.Remove(commodity, canSell - removed);
			}

			Debug.Assert(removed == canSell);
			return canSell;
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
