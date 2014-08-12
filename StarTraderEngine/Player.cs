namespace StarTrader
{
	using System.Collections.Generic;

	class Player
	{
		private readonly string m_name;
		private readonly List<Spaceship> m_ships = new List<Spaceship>();

		public Player(string name)
		{
			m_name = name;
			Cash = Game.Scenario.StartingCash;
		}

		public string Name
		{
			get { return m_name; }
		}

		public int Cash { get; private set; }

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
