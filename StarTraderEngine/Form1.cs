using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StarTrader
{
	using System.Linq;

	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
            /*
			Game.Players = new List<Player>();
			var player1 = new Player("Player1", 3, 3, 3);
			Game.Players.Add(player1);
			var player2 = new Player("Player2", 5, 3, 1);
			Game.Players.Add(player2);
			var player3 = new Player("Player3", 4, 4, 1);
			Game.Players.Add(player3);
			var player4 = new Player("Player4", 4, 4, 1);
			Game.Players.Add(player4);

			var biddingStage = new BiddingStage();
			biddingStage.InitiativeBid(player1, 20);
			biddingStage.InitiativeBid(player2, 2);
			biddingStage.CommodityBid(player1, Game.StarSystems[StarSystemType.EpsilonEridani][Commodity.Polymer], 2, 10, true);
			biddingStage.CommodityBid(player2, Game.StarSystems[StarSystemType.EpsilonEridani][Commodity.Polymer], 3, 11, true);
			biddingStage.CommodityBid(player3, Game.StarSystems[StarSystemType.EpsilonEridani][Commodity.Polymer], 2, 5, false);
			biddingStage.CommodityBid(player3, Game.StarSystems[StarSystemType.EpsilonEridani][Commodity.Polymer], 1, 6, false);

			var initiativeStage = biddingStage.NextStage();
			initiativeStage.SetInitiative();

			var transactionStage = initiativeStage.NextStage();


			transactionStage.PerformTransactions(Game.StarSystems[StarSystemType.EpsilonEridani][Commodity.Isotope]);
			transactionStage.PerformTransactions(Game.StarSystems[StarSystemType.EpsilonEridani][Commodity.Polymer]);
             */
		}
	}
}
