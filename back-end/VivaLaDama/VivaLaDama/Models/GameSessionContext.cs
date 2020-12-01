using Microsoft.EntityFrameworkCore;

namespace VivaLaDama.Models
{
    public class GameSessionContext : DbContext
    {
        public GameSessionContext(DbContextOptions<GameSessionContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Chessboard>();

            modelBuilder.Entity<Move>().OwnsOne(move => move.Target);
            modelBuilder.Entity<Move>().OwnsOne(move => move.From);
            modelBuilder.Entity<Move>().OwnsOne(move => move.To);
        }
        public DbSet<GameSession> GameSessions { get; set; }
    }
}