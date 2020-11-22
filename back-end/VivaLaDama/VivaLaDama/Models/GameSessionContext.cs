using Microsoft.EntityFrameworkCore;

namespace VivaLaDama.Models
{
    public class GameSessionContext : DbContext
    {
        public GameSessionContext(DbContextOptions<GameSessionContext> options) : base(options)
        {
        }

        public DbSet<GameSession> GameSessions { get; set; }
    }
}
