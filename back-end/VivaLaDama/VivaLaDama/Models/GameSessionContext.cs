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
            modelBuilder.Ignore<Coordinate>();
            modelBuilder.Ignore<Pawn>();

            modelBuilder.Entity<GameSession>().HasKey(game => game.IdGame);
            modelBuilder.Entity<Move>().HasKey(move => move.IdMossa);
        }
        public DbSet<GameSession> GameSessions { get; set; }
    }
}