using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace StarTrader.Entities
{
    public class GameEntry
    {
        public GameEntry()
        { }

        public GameEntry(string ownerId, string gameName)
        {
            OwnerId = ownerId;
            GameName = gameName;
            DateCreated = DateTimeOffset.UtcNow;
        }

        private readonly List<string> _playerIds = new List<string>();

        public int Id { get; private set; }

        public DateTimeOffset DateCreated { get; private set; }

        [Required]
        public string OwnerId { get; private set; }

        [Required]
        public string GameName { get; private set; }

        [Column("PlayerIds")]
        private string PlayerIdsAsString
        {
            get 
            {
                return string.Join(",", _playerIds.Distinct());
            }

            set
            {
                _playerIds.RemoveAll(s => true);

                if (value != null)
                {
                    _playerIds.AddRange(value.Split(',').Distinct().OrderBy(s => s));
                }
            }
        }

        internal static readonly Expression<Func<GameEntry, string>> 
            PlayerIdsAsStringExpression = g => g.PlayerIdsAsString;

        public IEnumerable<string> PlayerIds
        {
            get { return _playerIds.Distinct().OrderBy(s => s); }
        }

        public void AddPlayer(string playerId)
        {
            _playerIds.Add(playerId);
        }

        public void RemovePlayer(string playerId)
        {
            _playerIds.Remove(playerId);
        }
    }
}