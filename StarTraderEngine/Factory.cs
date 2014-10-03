namespace StarTrader
{
    public class Factory
    {
        public Factory(Player owner, Commodity commodity)
        {
            Owner = owner;
            Commodity = commodity;
        }

        public Player Owner { get; private set; }

        public Commodity Commodity { get; private set; }

        public int Capacity { get; set; }

        public int GetPrice(StarSystem starSystem)
        {
            return (10 + 5 * starSystem[Commodity].Price) * Capacity;
        }
    }
}
