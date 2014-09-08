
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using StarTrader.Entities;

namespace StarTrader.Models
{
    public class GameEntryViewModel
    {
        private readonly Dictionary<string, string> _userNames;

        public GameEntryViewModel(Dictionary<string, string> userNames, IList<GameEntry> gameEntries)
        {
            Debug.Assert(userNames != null, "userNames is null");
            Debug.Assert(gameEntries != null, "gameEntries is null");

            _userNames = userNames;
            GameEntries = gameEntries;
        }

        public IList<GameEntry> GameEntries { get; private set; }
            
        [Required]
        [MinLength(1)]
        public string NewGameName { get; set; }

        public string GetUserName(string userId)
        {
            string userName;
            return _userNames.TryGetValue(userId, out userName)
                ? userName
                : string.Empty;
        }
    }
}