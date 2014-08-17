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
		    var game = new Game();
            game.Initialize(
                new[] { "Player1", "Player2", "Player3", "Player4" }, 
                new FreeTradeScenario());

		    var player1 = game.Players[0];
            var player2 = game.Players[1];
            var player3 = game.Players[2];
            var player4 = game.Players[3];

            player1.Reputation.PoliticalTies = 3;
            player1.Reputation.EconomicTies = 3;
            player1.Reputation.CriminalTies = 3;

            player2.Reputation.PoliticalTies = 5;
            player2.Reputation.EconomicTies = 3;
            player2.Reputation.CriminalTies = 1;

            player3.Reputation.PoliticalTies = 4;
            player3.Reputation.EconomicTies = 4;
            player3.Reputation.CriminalTies = 1;

            player4.Reputation.PoliticalTies = 4;
            player4.Reputation.EconomicTies = 4;
            player4.Reputation.CriminalTies = 1;


			var biddingStage = new BiddingStage(game);
			biddingStage.InitiativeBid(player1, 20);
			biddingStage.InitiativeBid(player2, 2);
			biddingStage.CommodityBid(player1, game.StarSystems[StarSystemType.EpsilonEridani][Commodity.Polymer], 2, 10, true);
			biddingStage.CommodityBid(player2, game.StarSystems[StarSystemType.EpsilonEridani][Commodity.Polymer], 3, 11, true);
			biddingStage.CommodityBid(player3, game.StarSystems[StarSystemType.EpsilonEridani][Commodity.Polymer], 2, 5, false);
			biddingStage.CommodityBid(player3, game.StarSystems[StarSystemType.EpsilonEridani][Commodity.Polymer], 1, 6, false);

			var initiativeStage = biddingStage.NextStage();
			initiativeStage.SetInitiative();

			var transactionStage = initiativeStage.NextStage();


			transactionStage.PerformTransactions(game.StarSystems[StarSystemType.EpsilonEridani][Commodity.Isotope]);
			transactionStage.PerformTransactions(game.StarSystems[StarSystemType.EpsilonEridani][Commodity.Polymer]);
		}
	}
}
