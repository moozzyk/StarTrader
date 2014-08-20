namespace StarTrader
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class GameEvent
    {
        private const int Price = 5;

        private readonly int m_delay;
        private readonly Connections m_requiredConnections;
        private readonly bool m_reusable;
        private readonly string m_description;
        private readonly HashSet<Player> m_knownTo = new HashSet<Player>();

        private int m_turnToActivate;

        public GameEvent(int delay, Connections requiredConnections, bool reusable, string description)
        {
            m_delay = delay;
            m_requiredConnections = requiredConnections;
            m_reusable = reusable;
            m_description = description;
        }

        public Connections RequiredConnections
        {
            get { return m_requiredConnections; }
        }

        public string Description
        {
            get { return m_description; }
        }

        public bool Deactivate(Game game)
        {
            if (m_turnToActivate < game.Turn)
            {
                Debug.Assert(game.CurrentEvents.Contains(this));
                Debug.Assert(!game.AvailableEvents.Contains(this));
                game.CurrentEvents.Remove(this);
                if (m_reusable)
                {
                    game.AvailableEvents.Add(this);
                }

                m_turnToActivate = 0;
                m_knownTo.Clear();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Called during the event phase
        /// </summary>
        public bool Activate(Game game)
        {
            if (m_turnToActivate == game.Turn)
            {
                // do stuff
                return true;
            }

            return false;
        }

        /// <summary>
        /// Invoked when the event is drawn from the pool
        /// </summary>
        public void Draw(Game game)
        {
            Debug.Assert(m_turnToActivate == 0);
            Debug.Assert(!game.CurrentEvents.Contains(this));
            Debug.Assert(game.AvailableEvents.Contains(this));
            Debug.Assert(m_knownTo.Count == 0);
            m_turnToActivate = game.Turn + m_delay;
            game.CurrentEvents.Add(this);
            game.AvailableEvents.Remove(this);
        }

        public bool Reveal(Player player)
        {
            if (IsKnownTo(player))
            {
                return true;
            }

            if (player.Cash < Price)
            {
                return false;
            }

            if (!player.Reputation.MeetsRequirements(RequiredConnections))
            {
                return false;
            }

            m_knownTo.Add(player);
            player.Cash -= Price;

            return true;
        }

        public bool IsKnownTo(Player player)
        {
            Debug.Assert(m_turnToActivate > 0);
            return m_knownTo.Contains(player);
        }

        public static readonly GameEvent[] AllEvents =
        {
            // Opportunities
            new Opportunity(3, new Connections.Criminal(4), true, "Slaves", Commodity.Slaves, 1, StarSystemType.MuHerculis) { Destination = StarSystemType.EpsilonEridani },
            new Opportunity(2, new Connections.Criminal(8), true, "Weapons", Commodity.Weapons, 3, StarSystemType.EpsilonEridani) { Destination = StarSystemType.MuHerculis },
            new Opportunity(1, new Connections.Criminal(1), true, "Weapons", Commodity.Weapons, 10, StarSystemType.TauCeti) { Destination = StarSystemType.GammaLeporis },
            new Opportunity.Module(1, new Connections.Criminal(1), true,  "Combat comm unit", StarSystemType.GammaLeporis) { ShipModuleType.CommUnit },
            new Opportunity(2, new Connections.Economic(2), true, "Furs", Commodity.Furs, 10, StarSystemType.MuHerculis)
            {
                Destination = StarSystemType.BetaHydri, 
                Location = SpaceShipLocation.Port,
                SellPrice = 30,
                Limit = 10
            }, // Buy (10) SPACE PORT Mu Herkulis, sell (30) SPACE PORT Beta Hydri. Can buy 10 units, does not take cargo space.
            new Opportunity(2, new Connections.Criminal(5), true, "Slaves", Commodity.Slaves, 2, StarSystemType.MuHerculis) { Destination = StarSystemType.BetaHydri },
            new Opportunity(1, new Connections.Criminal(6), true, "Drugs", Commodity.Drugs, 15, StarSystemType.BetaHydri) { Destination = StarSystemType.GammaLeporis },
            new Opportunity.EnvoyTransport(1, new Connections.Political(5), true, "Envoy transport", StarSystemType.EpsilonEridani, 20) 
            {
                Destination = StarSystemType.MuHerculis,
                Location = SpaceShipLocation.Port,
                SellPrice = 200
            },
            // Transport the envoy and his companions from SPACE PORT Epsilon Eridani to SPACE PORT Mu Herkulis. 
            // Requires 1 passenger module. Requires REPUTATION 20 or more. Receives 200 HT after arrival to Mu Herkulis.
            new Opportunity.Delivery(2, new Connections.Economic(10), true, "Unknown alien plants", StarSystemType.GammaLeporis)
            {
                Destination = StarSystemType.EpsilonEridani,
                Location = SpaceShipLocation.Port,
                SellPrice = 150,
                ReputationBoost = 3,
                RequiredFreight = 1
            }, 
            // Plants to be picked up from SPACE PORT Gamma Leporis and delivered to SPACE PORT Epsilon Eridani.
            // Use 1 Freight module (can't be stored in hull storage). Receives 150 HT after delivery and adds 3 to REPUTATION.
            new Opportunity(1, new Connections.Political(10), true, "Slaves", Commodity.Slaves, 1, StarSystemType.MuHerculis) { Destination = StarSystemType.GammaLeporis },
            new Opportunity(3, new Connections.Criminal(3), true,  "Slaves", Commodity.Slaves, 2, StarSystemType.EpsilonEridani) { Destination = StarSystemType.GammaLeporis },
            new Opportunity(1, new Connections.Criminal(6), true,  "Drugs", Commodity.Drugs, 12, StarSystemType.EpsilonEridani) { Destination = StarSystemType.GammaLeporis },
            new Opportunity.Hull(1, new Connections.Criminal(10), true,  "Hull Javelin", StarSystemType.BetaHydri, HullType.Javelin, 5), // Hull Javelin and/or 5 black market modules. Available in Beta Hydri.
            new Opportunity.Module(4, new Connections.Criminal(8), true,  "Black market modules", StarSystemType.TauCeti)
            {
                ShipModuleType.Arsenal,
                ShipModuleType.CommUnit,
                ShipModuleType.HeavyWeapons,
                ShipModuleType.Hunter,
            }, // One each type. Available on Tau Ceti.
            new Opportunity.ResearchExpedition(2, new Connections.Political(2), true, "Research expedition"),
            new Opportunity.Hull(4, new Connections.Economic(3), true, "Hull Dagger", StarSystemType.TauCeti, HullType.Dagger, 1), // Hull Dagger and/or 1 black market module. Available on Tau Ceti.
            new Opportunity(3, new Connections.Economic(8), true, "Weapons", Commodity.Weapons, 8, StarSystemType.EpsilonEridani) { Destination = StarSystemType.GammaLeporis },
            new Opportunity(2, new Connections.Economic(2), true, "Weapons", Commodity.Weapons, 3, StarSystemType.BetaHydri) { Destination = StarSystemType.GammaLeporis },
            new Opportunity(2, new Connections.Criminal(8), true,  "Drugs", Commodity.Drugs, 10, StarSystemType.TauCeti) { Destination = StarSystemType.SigmaDraconis },
            new Opportunity.Hull(2, new Connections.Economic(10), true, "Any black market hull", StarSystemType.MuHerculis, null, 5), // Any black market hull and/or 5 modules. Available on Mu Herkulis.
            new Opportunity.Hull(2, new Connections.Criminal(8), true,  "Hull Sword", StarSystemType.MuHerculis, HullType.Sword, 3), // Hull Sword and/or 3 black market modules. Available on Mu Herkulis.
            new Opportunity.Delivery(1, new Connections.Economic(5), true, "Unique animals", StarSystemType.MuHerculis)
            {
                Destination = StarSystemType.EpsilonEridani,
                Location = SpaceShipLocation.Port,
                SellPrice = 150,
                RequiredPassenger = 1
            },
            // Pick up from SPACE PORT Mu Herkulis and delivery to SPACE PORT Epsilon Eridani. Require 1 passenger module. Reward after arrival - 150 HT.
            new Opportunity(2 ,new Connections.Criminal(5), true,  "Weapons", Commodity.Weapons, 3, StarSystemType.EpsilonEridani) { Destination = StarSystemType.MuHerculis },
            new Opportunity.Hull(3, new Connections.Criminal(4), true,  "Hull Dagger", StarSystemType.SigmaDraconis, HullType.Dagger, 1), // Hull Dagger and/or 1 black market moduł. Available on Sigma Draconis.
            new Opportunity(1, new Connections.Political(7), true, "Weapons", Commodity.Weapons, 5, StarSystemType.BetaHydri) { Destination = StarSystemType.MuHerculis },
            new Opportunity.Module(1, new Connections.Political(5), true, "Black market modules", StarSystemType.EpsilonEridani)
            {
                ShipModuleType.Arsenal,
                ShipModuleType.CommUnit,
                ShipModuleType.HeavyWeapons,
                ShipModuleType.Hunter,
            },
            // One each type. Available on Epsilon Eridani.

            // Events
            new GameEvent(1, new Connections.Economic(5), false, "Psychic disruption"), // Substract 4 from dice rolls in hyperjump phase
            new GameEvent(1, new Connections.Political(10), true, "Alien race"), // Border planets under attack. Panic ensues. All prices go down by 3.
            new GameEvent(4, new Connections.Political(3), false, "Galactic war"), // Component prices +3. Polymers +6. Isotopes +2. During this turn (and next) ships cannot be bought (including black market). Ignore applicable events. Add 5 to the die roll for spaceship sellers during these stages. Increment Police Efficiency by 4 during this turn.
            new GameEvent(3, new Connections.Criminal(3),  true, "Inflation"), // Fake bank notes flood the market. Cut everyone's cash by 50% (round up). Also cut all unpaid loans (don't change interest).
            new GameEvent(2, new Connections.Political(2), false, "Civil war"), // Civil war in Gamma Leporis. Weapons prices sold on the Gamma Leporis planet increase 3x. All ships in that space port and all factories and warehouses on Gamma Leporis are nationalized - discard them. Owners are compensated at 50% current value of the factories, warehouse prices and catalog price of the ships (hull and modules, not the crew). Commodities are lost.
            new GameEvent(4, new Connections.Political(3), false, "Colony"), // New planet in Mu Herkulis. All prices in the system go up by 5.
            new GameEvent(2, new Connections.Economic(6), true, "Technological breakthrough"), // In components. All Supply/demand modifiers go up by 3 through the end of the game.
            new GameEvent(3, new Connections.Economic(8), true, "Technological breakthrough"), // Food production; synthetic food is available. All Supply/demand modifiers go up by 2 through the end of the game.
            new GameEvent(2, new Connections.Economic(6), true, "Technological breakthrough"), // Isotope production. All Supply/demand modifiers go down by 3 through the end of the game, can't be lower than -10.
            new GameEvent(1, new Connections.Political(7), true, "Epidemic"), // Prices increase by 4. Can't hyperjump to and from space ports (only to/from the plant and space). Police efficiency and security level increase by during this turn.
            new GameEvent(4, new Connections.Political(8), false, "Inspection"), // Federal govt interrogates everyone. Decrement everyone's Reputation by 2D (each player rolls separately).
            new GameEvent(1, new Connections.Criminal(10),  false, "Pirate attack"), // On Mu Herkulis. All commodities and modules in warehouses are lost. Roll 1D for each ship in space port in Mu Herkulis. If <= 3 - the ship and everything onboard is lost, >=4 ship escapes into inter-planetary space. Police efficiency and Security level go up by 3 during this turn.
            new GameEvent(4, new Connections.Political(3), false, "Special tax"), // Every player immediately pays 1 HT for each warehouse capacity unit, 2 HT for each unit of factory capacity and 5 HT for each space ship. Up to available cash.
            new GameEvent(4, new Connections.Economic(3), true, "Discovery") // New star system with developed polymer production. Polymer prices -5. Component and isotope +2.
        };
    }
}
