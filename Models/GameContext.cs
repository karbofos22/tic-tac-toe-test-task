using Microsoft.EntityFrameworkCore;

namespace tic_tac_toe_testTask.Models
{
    public class GameContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
