namespace TestApp
{
	using System;
	using System.Collections.Generic;
	using System.Windows.Forms;

	using StarTrader;

	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Game game = new Game();
			game.Initialize(new[] { "Player1", "Player2", "Player3", "Player4" }, new FreeTradeScenario());
			Player player1 = game.Players.Find(p => p.Name == "Player1");
			Player player2 = game.Players.Find(p => p.Name == "Player2");
			Player player3 = game.Players.Find(p => p.Name == "Player3");
			Player player4 = game.Players.Find(p => p.Name == "Player4");

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
