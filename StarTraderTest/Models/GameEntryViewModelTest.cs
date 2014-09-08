
using System.Collections.Generic;
using StarTrader.Entities;
using Xunit;

namespace StarTrader.Models
{
    public class GameEntryViewModelTest
    {
        [Fact]
        public void Can_get_game_entries()
        {
            var gameEntries = new List<GameEntry>();
            var model = new GameEntryViewModel(new Dictionary<string, string>(), gameEntries);

            Assert.Same(gameEntries, model.GameEntries);
        }

        [Fact]
        public void Can_get_set_game_name()
        {
            var model = new GameEntryViewModel(new Dictionary<string, string>(), new List<GameEntry>())
            {
                NewGameName = "new game"
            };

            Assert.Equal("new game", model.NewGameName);
        }

        [Fact]
        public void Can_resolve_exisiting_user()
        {
            var model = new GameEntryViewModel(new Dictionary<string, string> {{"07", "Borewicz"}},
                new List<GameEntry>());

            Assert.Equal("Borewicz", model.GetUserName("07"));
        }

        [Fact]
        public void Resolving_non_existing_user_returns_empty_name()
        {
            var model = new GameEntryViewModel(new Dictionary<string, string>(), new List<GameEntry>());

            Assert.Equal(string.Empty, model.GetUserName("007"));
        }
    }
}
