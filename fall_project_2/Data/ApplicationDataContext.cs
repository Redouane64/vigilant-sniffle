using System.Text.Json;

using fall_project_2;

using Microsoft.EntityFrameworkCore;
/* using Microsoft.EntityFrameworkCore.Infrastructure; */

public class ApplicationDataContext : DbContext
{
    public ApplicationDataContext(/* DbContextOptions<ApplicationDataContext> options */)
        : base(/* options */)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=./app.db");

        // create database if it does not exist
        Database.EnsureCreated();

        // apply any migrations
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .ToTable("users")
            .HasIndex(u => u.Email, "idx_user_email")
            .IsUnique();

        modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("id");
        modelBuilder.Entity<User>().Property(u => u.Name).HasColumnName("name");
        modelBuilder.Entity<User>().Property(u => u.Email).HasColumnName("email");

        modelBuilder.Entity<User>()
            .HasMany(u => u.Wallets)
            .WithOne(w => w.User)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey("fk_user_wallets");

        modelBuilder.Entity<Wallet>()
            .ToTable("wallets");

        modelBuilder.Entity<Wallet>()
            .Property(u => u.Id).HasColumnName("id");

        modelBuilder.Entity<Wallet>()
            .Property(w => w.Name).HasColumnName("name");

        modelBuilder.Entity<Wallet>()
            .Property(w => w.Amount)
            .HasColumnName("amount")
            .HasColumnType("TEXT")
            .HasConversion(
                (Money value) => JsonSerializer.Serialize(value, new JsonSerializerOptions()),
                (String value) => JsonSerializer.Deserialize<Money>(value, new JsonSerializerOptions())
            );

        modelBuilder.Entity<Wallet>()
            .Property(w => w.Currency)
            .HasColumnName("currency");

        modelBuilder.Entity<Wallet>()
            .HasMany(w => w.Operations)
            .WithOne(o => o.Wallet)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey("fk_wallet_operations");

        modelBuilder.Entity<Operation>()
            .Property(o => o.Id).HasColumnName("id");


        modelBuilder.Entity<Operation>()
            .Property(o => o.Date).HasColumnName("date");


        modelBuilder.Entity<Operation>()
            .Property(o => o.Value).HasColumnName("value")
            .HasConversion(
                (Money value) => JsonSerializer.Serialize(value, new JsonSerializerOptions()),
                (String value) => JsonSerializer.Deserialize<Money>(value, new JsonSerializerOptions())
            );

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
}