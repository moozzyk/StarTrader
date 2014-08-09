using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarTrader
{
	class Factory
	{
		public Factory(Player owner, Commodity commodity)
		{
			Owner = owner;
			Commodity = commodity;
		}

		public Player Owner { get; private set; }

		public Commodity Commodity { get; private set; }

		public int Capacity { get; set; }
	}
}
