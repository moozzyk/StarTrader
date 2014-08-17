namespace StarTrader
{
    using System.Collections.Generic;

    internal class StarSystemFactory
    {
        private static readonly StarSystem GammaLeporis =
            new StarSystem(
                StarSystemType.GammaLeporis, 
                new[] {Commodity.Polymer},
                new[] { new CommodityMarket(Commodity.Polymer, 2, 1) });

        private static readonly StarSystem EpsilonEridani =
            new StarSystem(StarSystemType.EpsilonEridani,
                new Commodity[0],
                new[]
                {
                    new CommodityMarket(Commodity.Polymer, 5, -7),
                    new CommodityMarket(Commodity.Isotope, 10, -7),
                    new CommodityMarket(Commodity.Food, 14, -8),
                    new CommodityMarket(Commodity.Component, 17, -9)
                });

        private static readonly StarSystem TauCeti =
            new StarSystem(
                StarSystemType.TauCeti,
                new[] {Commodity.Isotope},
                new[]
                {
                    new CommodityMarket(Commodity.Polymer, 6, -10),
                    new CommodityMarket(Commodity.Isotope, 5, -9)
                });

        private static readonly StarSystem BetaHydri =
            new StarSystem(
                StarSystemType.BetaHydri,
                new Commodity[0],
                new[]
                {
                    new CommodityMarket(Commodity.Polymer, 6, -8),
                    new CommodityMarket(Commodity.Isotope, 10, -8),
                    new CommodityMarket(Commodity.Food, 15, -9),
                    new CommodityMarket(Commodity.Component, 18, -10)
                });

        private static readonly StarSystem SigmaDraconis =
            new StarSystem(
                StarSystemType.SigmaDraconis,
                new[] {Commodity.Isotope, Commodity.Food},
                new[]
                {
                    new CommodityMarket(Commodity.Polymer, 8, -5),
                    new CommodityMarket(Commodity.Isotope, 4, -1),
                    new CommodityMarket(Commodity.Food, 10, -3),
                    new CommodityMarket(Commodity.Component, 14, -8)
                });

        private static readonly StarSystem MuHerculis =
            new StarSystem(
                StarSystemType.MuHerculis,
                new[] {Commodity.Component},
                new[]
                {
                    new CommodityMarket(Commodity.Polymer, 8, -9),
                    new CommodityMarket(Commodity.Isotope, 11, 1),
                    new CommodityMarket(Commodity.Component, 12, -4)
                });


        public static Dictionary<StarSystemType, StarSystem> CreateStarSystems()
		{
			// TODO - convert to modifier table?
			GammaLeporis.HyperJumpSuccessChance[EpsilonEridani] = 6;
			GammaLeporis.HyperJumpSuccessChance[TauCeti] = 5;
			GammaLeporis.HyperJumpSuccessChance[BetaHydri] = 5;
			GammaLeporis.HyperJumpSuccessChance[SigmaDraconis] = 4;
			GammaLeporis.HyperJumpSuccessChance[MuHerculis] = 2;

			EpsilonEridani.HyperJumpSuccessChance[GammaLeporis] = 7;
			EpsilonEridani.HyperJumpSuccessChance[TauCeti] = 9;
			EpsilonEridani.HyperJumpSuccessChance[BetaHydri] = 8;
			EpsilonEridani.HyperJumpSuccessChance[SigmaDraconis] = 7;
			EpsilonEridani.HyperJumpSuccessChance[MuHerculis] = 6;

			TauCeti.HyperJumpSuccessChance[GammaLeporis] = 7;
			TauCeti.HyperJumpSuccessChance[EpsilonEridani] = 9;
			TauCeti.HyperJumpSuccessChance[BetaHydri] = 8;
			TauCeti.HyperJumpSuccessChance[SigmaDraconis] = 7;
			TauCeti.HyperJumpSuccessChance[MuHerculis] = 6;

			BetaHydri.HyperJumpSuccessChance[GammaLeporis] = 7;
			BetaHydri.HyperJumpSuccessChance[EpsilonEridani] = 8;
			BetaHydri.HyperJumpSuccessChance[TauCeti] = 8;
			BetaHydri.HyperJumpSuccessChance[SigmaDraconis] = 6;
			BetaHydri.HyperJumpSuccessChance[MuHerculis] = 6;

			SigmaDraconis.HyperJumpSuccessChance[GammaLeporis] = 4;
			SigmaDraconis.HyperJumpSuccessChance[EpsilonEridani] = 5;
			SigmaDraconis.HyperJumpSuccessChance[TauCeti] = 5;
			SigmaDraconis.HyperJumpSuccessChance[BetaHydri] = 4;
			SigmaDraconis.HyperJumpSuccessChance[MuHerculis] = 6;

			MuHerculis.HyperJumpSuccessChance[GammaLeporis] = 2;
			MuHerculis.HyperJumpSuccessChance[EpsilonEridani] = 4;
			MuHerculis.HyperJumpSuccessChance[TauCeti] = 4;
			MuHerculis.HyperJumpSuccessChance[BetaHydri] = 3;
			MuHerculis.HyperJumpSuccessChance[SigmaDraconis] = 5;

            return new Dictionary<StarSystemType, StarSystem>
            {
                {StarSystemType.GammaLeporis, GammaLeporis},
                {StarSystemType.EpsilonEridani, EpsilonEridani},
                {StarSystemType.TauCeti, TauCeti},
                {StarSystemType.BetaHydri, BetaHydri},
                {StarSystemType.SigmaDraconis, SigmaDraconis},
                {StarSystemType.MuHerculis, MuHerculis}
            };
		}
    }
}
