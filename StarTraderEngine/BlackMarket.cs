using System;
using System.Diagnostics;

namespace StarTrader
{
    public class BlackMarket
    {
        private static readonly int[][] SellPrice =
        {
            new[] {8, 30, 12, 5},
            new[] {10, 30, 15, 8},
            new[] {15, 30, 25, 10},
            new[] {25, 30, 35, 15},
            new[] {30, 30, 50, 20},
            new[] {40, 30, 70, 25}
        };

        private readonly IDice m_dice;

        public BlackMarket(IDice dice)
        {
            m_dice = dice;
        }

        public int CalculatePrice(Commodity commodity)
        {
            if (!commodity.BlackMarket())
            {
                throw new InvalidOperationException("Black market can only be used with illegal goods");
            }

            Debug.Assert(commodity >= Commodity.Weapons && commodity <= Commodity.Slaves);
            int index = m_dice.Roll1Die();
            Debug.Assert(index >= 1 && index <= 6);
            return SellPrice[index - 1][commodity - Commodity.Weapons];
        }
    }
}