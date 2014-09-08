using System.Data.Entity;

namespace StarTrader.Entities
{
    public class StarTraderContext : DbContext
    {
        static StarTraderContext()
        {
            // TODO: Use migrations once the model gets stabled
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<StarTraderContext>());
        }

        public DbSet<GameEntry> GameEntries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameEntry>().Property(GameEntry.PlayerIdsAsStringExpression);
        }
    }
}