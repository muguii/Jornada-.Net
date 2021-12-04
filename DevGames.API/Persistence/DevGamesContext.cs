using DevGames.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevGames.API.Persistence
{
    public class DevGamesContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DevGamesContext(DbContextOptions<DevGamesContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // O mapeamento feito dessa maneira é denominado Fluent API

            modelBuilder.Entity<Board>().ToTable("BOARD").HasKey(prop => prop.Id);
            modelBuilder.Entity<Board>()
                .HasMany(prop => prop.Posts)
                .WithOne()
                .HasForeignKey(prop => prop.BoardId);

            modelBuilder.Entity<Post>().ToTable("POST").HasKey(prop => prop.Id);
            modelBuilder.Entity<Post>()
                .HasMany(prop => prop.Comments)
                .WithOne()
                .HasForeignKey(prop => prop.PostId);

            modelBuilder.Entity<Comment>().ToTable("COMMENT").HasKey(prop => prop.Id);
        }
    }
}
