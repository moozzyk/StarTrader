
namespace StarTrader
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class FreeTradeScenario : Scenario
    {
        //public void GetInitialTies(out int political, out int economic, out int criminal)
        //{
        //    // TODO - get it from UI
        //    int sum = Dice.Roll();
        //    political = sum / 3;
        //    economic = sum / 3;
        //    criminal = sum - political - economic;
        //}

        internal override IEnumerable<Player> CreatePlayers(string[] playerNames)
        {
            Debug.Assert(playerNames != null && playerNames.Length > 0, "playerNames is null or empty");

            /*
            new[]
            {
                new Spaceship(HullType.Clarinet, CrewClass.B)
                {
                    new ShipModule(ShipModuleType.Freight), 
                    new ShipModule(ShipModuleType.Freight), 
                    new ShipModule(ShipModuleType.Freight),
                    new ShipModule(ShipModuleType.Passenger),
                    new ShipModule(ShipModuleType.LightWeapons),
                    new ShipModule(ShipModuleType.SafeJump),
                }
            }*/

            // TODO: Add ships
            return playerNames.Select(playerName => new Player(playerName, 300, 20, -1, -1, -1));
        }
    }
}
