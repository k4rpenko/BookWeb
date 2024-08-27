using LibraryDAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryBLL
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<UserModel> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().Property(x => x.UserName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<UserModel>().Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<UserModel>().Property(x => x.Password)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<UserModel>().Property(x => x.Books)
                .HasColumnType("text[]");

            modelBuilder.Entity<UserModel>().Property(x => x.Role)
                .HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }
    }
}
