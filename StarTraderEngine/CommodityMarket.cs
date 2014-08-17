namespace StarTrader
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;

	public class CommodityMarket
	{
		public const int MaxPrice = 20;
		public const int MinPrice = 1;

		private const int StartingIndex = 18;
		private static readonly int[] SupplyDemandModifierMap = { 7, 6, 6, 6, 5, 5, 5, 4, 4, 4, 3, 3, 3, 2, 2, 1, 1, 0, 0, 0, -1, -1, -1, -2, -2, -2, -3, -3, -3, -4, -4, -4, -5, -5, -5, -5, -6 };

		private readonly List<Player> m_retailers = new List<Player>();
		private readonly List<Player> m_wholesalers = new List<Player>();
		private StarSystem m_system;

		public CommodityMarket(Commodity commodity, int price, int modifier)
		{
			Commodity = commodity;
			Price = price;
			SupplyDemandModifier = modifier;

			Debug.Assert(SupplyDemandModifierMap.Length == 37);
		}

		public Commodity Commodity { get; private set; }

		public int Price { get; private set; }

		public int SupplyDemandModifier { get; private set; }

		public Player Dictator { get; private set; }

		public IEnumerable<Player> Retailers
		{
			get { return m_retailers; }
		}

		public IEnumerable<Player> Wholesalers
		{
			get { return m_wholesalers; }
		}

		public StarSystem System
		{
			get
			{
				return m_system;
			}

			set
			{
				if (m_system != null)
				{
					throw new InvalidOperationException("Commodity markets cannot be moved between star systems");
				}

				m_system = value;
			}
		}

		public void PerformTransactions(IEnumerable<Transaction> transactions)
		{
			int currentModifierPosition = SupplyDemandModifier + Dice.Roll();

			var buyTransactions = transactions.Where(t => t is BuyTransaction).OrderByDescending(GetTransactionOrderByValue).ToList();
			var sellTransactions = transactions.Where(t => t is SellTransaction).OrderBy(GetTransactionOrderByValue).ToList();
			while (buyTransactions.Any() || sellTransactions.Any())
			{
				// allow buy transactions with negative modifier when there are no more sell offers
				if (buyTransactions.Any() && (currentModifierPosition >= 0 || !sellTransactions.Any()))
				{
					// check the highest buy offer
					var buyTransaction = buyTransactions.First();

					// calculate allowed quantity
					int mapPosition = FindLeftMostPriceModifier(buyTransaction.OfferPrice - Price);

					// the allowed quantity is the number of positions current modifier is to the right of the left-most position found above
					int allowedQuantity = currentModifierPosition - mapPosition;
					if (allowedQuantity > 0)
					{
						// buy and move the supply/demand modifier
						currentModifierPosition -= buyTransaction.Execute(allowedQuantity);
					}

					buyTransactions.Remove(buyTransaction);
				}

				// check if any sell transaction that came into money
				if (sellTransactions.Any() && (currentModifierPosition < 0 || !buyTransactions.Any()))
				{
					// check the lowest sell offer
					var sellTransaction = sellTransactions.First();

					// calculate allowed quantity
					int mapPosition = FindRightMostPriceModifier(sellTransaction.OfferPrice - Price);

					// the allowed quantity is the number of positions current modifier is to the left of the right-most position found above
					int allowedQuantity = mapPosition - currentModifierPosition;
					if (allowedQuantity > 0)
					{
						// sell and move the supply/demand modifier
						currentModifierPosition += sellTransaction.Execute(allowedQuantity);
					}

					sellTransactions.Remove(sellTransaction);
				}
			}

			Debug.Assert(currentModifierPosition >= -StartingIndex && currentModifierPosition <= StartingIndex);

			// adjust the price
			Price += SupplyDemandModifierMap[GetMapIndex(currentModifierPosition)];
			Price = Math.Min(MaxPrice, Math.Max(MinPrice, Price));
		}

		private int GetTransactionOrderByValue(Transaction transaction)
		{
			// to ensure players with higher intiative are considered first we'll adjust the orderby clause to 1000*price + initiative
			Debug.Assert(transaction.Player.Initiative < 1000); // don't cheat
			return transaction.OfferPrice * 1000 + Math.Min(transaction.Player.Initiative, 999);
		}

		private int GetMapIndex(int modifier)
		{
			return StartingIndex + Math.Max(Math.Min(modifier, StartingIndex), -StartingIndex);
		}

		private int FindLeftMostPriceModifier(int priceDifferential)
		{
			for (int current = 0; current < SupplyDemandModifierMap.Length; current++)
			{
				if (SupplyDemandModifierMap[current] == priceDifferential)
				{
					return current - StartingIndex;
				}
			}

			// right most
			return StartingIndex;
		}

		private int FindRightMostPriceModifier(int priceDifferential)
		{
			for (int current = SupplyDemandModifierMap.Length - 1; current >= 0; current--)
			{
				if (SupplyDemandModifierMap[current] == priceDifferential)
				{
					return current - StartingIndex;
				}
			}

			// left most
			return -StartingIndex;
		}

		public override int GetHashCode()
		{
			Debug.Assert(System != null, "Commodity market cannot be used without the star system");
			if (System == null)
			{
				return 0;
			}

			return System.GetHashCode() ^ Commodity.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Debug.Assert(System != null, "Commodity market cannot be used without the star system");
			var other = obj as CommodityMarket;
			if (System == null)
			{
				return false;
			}

			return other != null && Commodity.Equals(other.Commodity) && System.Equals(other.System);
		}

		public override string ToString()
		{
			return System + " - " + Commodity;
		}
	}
}
