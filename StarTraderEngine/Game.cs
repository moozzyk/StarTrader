using StarTrader.Events;

namespace StarTrader
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Game
    {
        public readonly Dictionary<StarSystemType, StarSystem> StarSystems;
        public readonly List<GameEvent> CurrentEvents;
        public readonly List<GameEvent> AvailableEvents;
        public readonly IDice Dice = new Dice.DiceImpl();

        public Game()
        {
            Turn = -1;
            StarSystems = StarSystemFactory.CreateStarSystems();
            CurrentEvents = new List<GameEvent>();
            AvailableEvents = GameEventFactory.CreateEvents(this);
            ShipTradeAllowed = true;
        }

        public void Initialize(string[] playerNames, Scenario scenario)
        {
            Debug.Assert(playerNames != null && playerNames.Length > 0, "playerNames is null or empty");
            Debug.Assert(scenario != null, "scenario is null");

            if (Turn >= 0)
            {
                throw new InvalidOperationException("Game has already been initialized.");
            }

            Turn = 0;
            Players = scenario.CreatePlayers(playerNames).ToList();

            // TODO: total initial ties
        }

        public int Turn { get; private set; }

        /// <summary>
        /// TODO move these into their own class
        /// </summary>
        public int HyperjumpModifier { get; set; }

        public Func<SpaceShipLocation, bool> HyperjumpAllowed { get; set; }

        public IEnumerable<Player> PlayersByInitiative { get { return Players.OrderByDescending(player => player.Initiative); } }

        public List<Player> Players { get; internal set; }

        public bool ShipTradeAllowed { get; set; }

        /// <summary>
        /// TODO - this and stuff in CivilWar should be moved to a proper class
        /// that calculates prices
        /// </summary>
        public int ShipTradeModifier { get; set; }
    }
}
