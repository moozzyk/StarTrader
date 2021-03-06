﻿using System;
using System.Linq;
using StarTrader.Events;

namespace StarTrader
{
    public class EventStage
    {
        private readonly Game m_game;

        public EventStage(Game game)
        {
            m_game = game;
        }

        public void Execute()
        {
            // deactivate expired events
            foreach (var gameEvent in m_game.CurrentEvents.ToList())
            {
                gameEvent.Deactivate();
            }

            // activate current events
            foreach (var gameEvent in m_game.CurrentEvents.ToList())
            {
                gameEvent.Activate();
            }

            // draw new ones
            int numberToDraw = m_game.Dice.Roll1Die() / 2;
            var random = new Random(Environment.TickCount);
            while (numberToDraw-- > 0 && m_game.AvailableEvents.Count > 0)
            {
                // todo - allow player to pick?
                int pick = random.Next(0, m_game.AvailableEvents.Count);
                m_game.AvailableEvents[pick].Draw();
            }
        }

        public bool CheckEvent(Player player, GameEvent gameEvent)
        {
            return gameEvent.Reveal(player);
        }
    }
}
