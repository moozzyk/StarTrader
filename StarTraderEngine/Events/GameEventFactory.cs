namespace StarTrader.Events
{
    using System.Collections.Generic;

    public class GameEventFactory
    {
        public static List<GameEvent> CreateEvents(Game game)
        {
            return new List<GameEvent>
            {
                // Opportunities
                new TradeOpportunity(game, 3, new Connections.Criminal(4), true, "Slaves", Commodity.Slaves, 1, StarSystemType.MuHerculis) { Destination = StarSystemType.EpsilonEridani },
                new TradeOpportunity(game, 2, new Connections.Criminal(8), true, "Weapons", Commodity.Weapons, 3, StarSystemType.EpsilonEridani) { Destination = StarSystemType.MuHerculis },
                new TradeOpportunity(game, 1, new Connections.Criminal(1), true, "Weapons", Commodity.Weapons, 10, StarSystemType.TauCeti) { Destination = StarSystemType.GammaLeporis },
                new ModuleOpportunity(game, 1, new Connections.Criminal(1), true,  "Combat comm unit", StarSystemType.GammaLeporis) { ShipModuleType.CommUnit },
                new TradeOpportunity(game, 2, new Connections.Economic(2), true, "Furs", Commodity.Furs, 10, StarSystemType.MuHerculis) { Destination = StarSystemType.BetaHydri, Limit = 10 },
                // Buy (10) SPACE PORT Mu Herkulis, sell (30) SPACE PORT Beta Hydri. Can buy 10 units, does not take cargo space.
                new TradeOpportunity(game, 2, new Connections.Criminal(5), true, "Slaves", Commodity.Slaves, 2, StarSystemType.MuHerculis) { Destination = StarSystemType.BetaHydri },
                new TradeOpportunity(game, 1, new Connections.Criminal(6), true, "Drugs", Commodity.Drugs, 15, StarSystemType.BetaHydri) { Destination = StarSystemType.GammaLeporis },
                new EnvoyTransport(game, 1, new Connections.Political(5), true, "Envoy transport", StarSystemType.EpsilonEridani, 20) 
                {
                    Destination = StarSystemType.MuHerculis,
                    Location = SpaceShipLocation.Port,
                    SellPrice = 200
                },
                // Transport the envoy and his companions from SPACE PORT Epsilon Eridani to SPACE PORT Mu Herkulis. 
                // Requires 1 passenger module. Requires REPUTATION 20 or more. Receives 200 HT after arrival to Mu Herkulis.
                new DeliveryOpportunity(game, 2, new Connections.Economic(10), true, "Unknown alien plants", StarSystemType.GammaLeporis)
                {
                    Destination = StarSystemType.EpsilonEridani,
                    Location = SpaceShipLocation.Port,
                    SellPrice = 150,
                    ReputationBoost = 3,
                    RequiredFreight = 1
                }, 
                // Plants to be picked up from SPACE PORT Gamma Leporis and delivered to SPACE PORT Epsilon Eridani.
                // Use 1 Freight module (can't be stored in hull storage). Receives 150 HT after delivery and adds 3 to REPUTATION.
                new TradeOpportunity(game, 1, new Connections.Political(10), true, "Slaves", Commodity.Slaves, 1, StarSystemType.MuHerculis) { Destination = StarSystemType.GammaLeporis },
                new TradeOpportunity(game, 3, new Connections.Criminal(3), true,  "Slaves", Commodity.Slaves, 2, StarSystemType.EpsilonEridani) { Destination = StarSystemType.GammaLeporis },
                new TradeOpportunity(game, 1, new Connections.Criminal(6), true,  "Drugs", Commodity.Drugs, 12, StarSystemType.EpsilonEridani) { Destination = StarSystemType.GammaLeporis },
                new HullOpportunity(game, 1, new Connections.Criminal(10), true,  "Hull Javelin", StarSystemType.BetaHydri, HullType.Javelin, 5),
                // Hull Javelin and/or 5 black market modules. Available in Beta Hydri.
                new ModuleOpportunity(game, 4, new Connections.Criminal(8), true,  "Black market modules", StarSystemType.TauCeti)
                {
                    ShipModuleType.Arsenal,
                    ShipModuleType.CommUnit,
                    ShipModuleType.HeavyWeapons,
                    ShipModuleType.Hunter,
                }, // One each type. Available on Tau Ceti.
                new ResearchExpedition(game, 2, new Connections.Political(2), true, "Research expedition"),
                new HullOpportunity(game, 4, new Connections.Economic(3), true, "Hull Dagger", StarSystemType.TauCeti, HullType.Dagger, 1),
                // Hull Dagger and/or 1 black market module. Available on Tau Ceti.
                new TradeOpportunity(game, 3, new Connections.Economic(8), true, "Weapons", Commodity.Weapons, 8, StarSystemType.EpsilonEridani) { Destination = StarSystemType.GammaLeporis },
                new TradeOpportunity(game, 2, new Connections.Economic(2), true, "Weapons", Commodity.Weapons, 3, StarSystemType.BetaHydri) { Destination = StarSystemType.GammaLeporis },
                new TradeOpportunity(game, 2, new Connections.Criminal(8), true,  "Drugs", Commodity.Drugs, 10, StarSystemType.TauCeti) { Destination = StarSystemType.SigmaDraconis },
                new HullOpportunity(game, 2, new Connections.Economic(10), true, "Any black market hull", StarSystemType.MuHerculis, null, 5),
                // Any black market hull and/or 5 modules. Available on Mu Herkulis.
                new HullOpportunity(game, 2, new Connections.Criminal(8), true,  "Hull Sword", StarSystemType.MuHerculis, HullType.Sword, 3),
                // Hull Sword and/or 3 black market modules. Available on Mu Herkulis.
                new DeliveryOpportunity(game, 1, new Connections.Economic(5), true, "Unique animals", StarSystemType.MuHerculis)
                {
                    Destination = StarSystemType.EpsilonEridani,
                    Location = SpaceShipLocation.Port,
                    SellPrice = 150,
                    RequiredPassenger = 1
                },
                // Pick up from SPACE PORT Mu Herkulis and delivery to SPACE PORT Epsilon Eridani. Require 1 passenger module. Reward after arrival - 150 HT.
                new TradeOpportunity(game, 2, new Connections.Criminal(5), true,  "Weapons", Commodity.Weapons, 3, StarSystemType.EpsilonEridani) { Destination = StarSystemType.MuHerculis },
                new HullOpportunity(game, 3, new Connections.Criminal(4), true,  "Hull Dagger", StarSystemType.SigmaDraconis, HullType.Dagger, 1),
                // Hull Dagger and/or 1 black market moduł. Available on Sigma Draconis.
                new TradeOpportunity(game, 1, new Connections.Political(7), true, "Weapons", Commodity.Weapons, 5, StarSystemType.BetaHydri) { Destination = StarSystemType.MuHerculis },
                new ModuleOpportunity(game, 1, new Connections.Political(5), true, "Black market modules", StarSystemType.EpsilonEridani)
                {
                    ShipModuleType.Arsenal,
                    ShipModuleType.CommUnit,
                    ShipModuleType.HeavyWeapons,
                    ShipModuleType.Hunter,
                },
                // One each type. Available on Epsilon Eridani.

                // Events
                new HyperjumpModifier(game, 1, new Connections.Economic(5), true, "Psychic disruption", -4), // Substract 4 from dice rolls in hyperjump phase
                new PriceModifier(game, 1, new Connections.Political(10), false, "Alien attack", c => -3, null), // Border planets under attack. Panic ensues. All prices go down by 3.
                new GameEvent(game, 4, new Connections.Political(3), true, "Galactic war"), // Component prices +3. Polymers +6. Isotopes +2. During this turn (and next) ships cannot be bought (including black market). Ignore applicable events. Add 5 to the die roll for spaceship sellers during these stages. Increment Police Efficiency by 4 during this turn.
                new GameEvent(game, 3, new Connections.Criminal(3),  false, "Inflation"), // Fake bank notes flood the market. Cut everyone's cash by 50% (round up). Also cut all unpaid loans (don't change interest).
                new GameEvent(game, 2, new Connections.Political(2), true, "Civil war"), // Civil war in Gamma Leporis. Weapons prices sold on the Gamma Leporis planet increase 3x. All ships in that space port and all factories and warehouses on Gamma Leporis are nationalized - discard them. Owners are compensated at 50% current value of the factories, warehouse prices and catalog price of the ships (hull and modules, not the crew). Commodities are lost.
                new PriceModifier(game, 4, new Connections.Political(3), true, "Colony", c => 5, s => s == StarSystemType.MuHerculis), // New planet in Mu Herkulis. All prices in the system go up by 5.
                new GameEvent(game, 2, new Connections.Economic(6), false, "Technological breakthrough"), // In components. All Supply/demand modifiers go up by 3 through the end of the game.
                new GameEvent(game, 3, new Connections.Economic(8), false, "Technological breakthrough"), // Food production; synthetic food is available. All Supply/demand modifiers go up by 2 through the end of the game.
                new GameEvent(game, 2, new Connections.Economic(6), false, "Technological breakthrough"), // Isotope production. All Supply/demand modifiers go down by 3 through the end of the game, can't be lower than -10.
                new GameEvent(game, 1, new Connections.Political(7), false, "Epidemic"), // Prices increase by 4. Can't hyperjump to and from space ports (only to/from the plant and space). Police efficiency and security level increase by during this turn.
                new GameEvent(game, 4, new Connections.Political(8), true, "Inspection"), // Federal govt interrogates everyone. Decrement everyone's Reputation by 2D (each player rolls separately).
                new GameEvent(game, 1, new Connections.Criminal(10),  true, "Pirate attack"), // On Mu Herkulis. All commodities and modules in warehouses are lost. Roll 1D for each ship in space port in Mu Herkulis. If <= 3 - the ship and everything onboard is lost, >=4 ship escapes into inter-planetary space. Police efficiency and Security level go up by 3 during this turn.
                new GameEvent(game, 4, new Connections.Political(3), true, "Special tax"), // Every player immediately pays 1 HT for each warehouse capacity unit, 2 HT for each unit of factory capacity and 5 HT for each space ship. Up to available cash.
                new PriceModifier(game, 4, new Connections.Economic(3), false, "Discovery", c => (c == Commodity.Component || c == Commodity.Isotope) ? 2 : (c == Commodity.Polymer ? -5 : 0), null) // New star system with developed polymer production. Polymer prices -5. Component and isotope +2.
            };
        }
    }
}
