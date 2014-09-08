
using System;
using Xunit;

namespace StarTrader.Entities
{
    public class GameEntryTests
    {
        [Fact]
        public void GameEntry_initialized_correctly()
        {
            var gameEntry = new GameEntry("abc", "my game");

            Assert.Equal("abc", gameEntry.OwnerId);
            Assert.Equal("my game", gameEntry.GameName);
            Assert.True(gameEntry.DateCreated > DateTimeOffset.Now.AddSeconds(-1) && 
                gameEntry.DateCreated < DateTimeOffset.UtcNow);
        }

        [Fact]
        public void Can_add_remove_player()
        {
            var gameEntry = new GameEntry();
            Assert.Empty(gameEntry.PlayerIds);

            gameEntry.AddPlayer("a");
            Assert.Equal(new[] {"a"}, gameEntry.PlayerIds);

            gameEntry.RemovePlayer("a");
            Assert.Empty(gameEntry.PlayerIds);
        }

        [Fact]
        public void Removing_non_existing_player_is_no_op()
        {
            var gameEntry = new GameEntry();

            gameEntry.RemovePlayer("X");
            Assert.Empty(gameEntry.PlayerIds);

            gameEntry.AddPlayer("a");
            gameEntry.RemovePlayer("X");

            Assert.Equal(new[] {"a"}, gameEntry.PlayerIds);
        }
    }
}
