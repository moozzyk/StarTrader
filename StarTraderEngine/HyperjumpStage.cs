namespace StarTrader.Events
{
    public class HyperjumpStage
    {
        private readonly Game m_game;

        public HyperjumpStage(Game game)
        {
            m_game = game;
        }

        public void Hyperjump(Spaceship ship, StarSystem destination)
        {
            int diceModifier = m_game.HyperjumpModifier;
        }
    }
}
