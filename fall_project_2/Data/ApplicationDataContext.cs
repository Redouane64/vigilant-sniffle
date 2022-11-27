namespace fall_project_2.Data;

using System.Text.Json;

using Microsoft.EntityFrameworkCore;
/* using Microsoft.EntityFrameworkCore.Infrastructure; */

public class ApplicationDataContext : DbContext
{
    public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options)
        : base(options)
    { }

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
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Wallet>()
            .ToTable("wallets");

        modelBuilder.Entity<Wallet>()
            .Property(u => u.Id).HasColumnName("id");

        modelBuilder.Entity<Wallet>()
            .Property(w => w.Name).HasColumnName("name");

        modelBuilder.Entity<Wallet>()
            .Property(w => w.Currency)
            .HasConversion<string>()
            .HasColumnName("currency");

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
            .Property("UserId")
            .HasColumnName("user_id");

        modelBuilder.Entity<Wallet>()
            .HasMany(w => w.Operations)
            .WithOne(o => o.Wallet)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Operation>()
            .Property("WalletId")
            .HasColumnName("wallet_id");

        modelBuilder.Entity<Operation>()
                    .ToTable("operations")
                    .HasDiscriminator<string>("operation_type")
                    .HasValue("expense")
                    .HasValue("income");

        modelBuilder.Entity<Operation>()
            .Property(o => o.Id).HasColumnName("id");


        modelBuilder.Entity<Operation>()
            .Property(o => o.Date).HasColumnName("date");


        modelBuilder.Entity<Operation>()
            .Property(o => o.Value).HasColumnName("value")
            .HasConversion(
                (Money value) => JsonSerializer.Serialize(value, new JsonSerializerOptions()),
                (String value) => JsonSerializer.Deserialize<Money>(value, new JsonSerializerOptions())
            )
            .IsRequired();

        modelBuilder.Entity<Income>().HasBaseType<Operation>();
        modelBuilder.Entity<Expense>().HasBaseType<Operation>();

        modelBuilder.Entity<Income>().Property(e => e.Type).HasColumnName("income_type").HasConversion<string>();
        modelBuilder.Entity<Expense>().Property(e => e.Type).HasColumnName("expense_type").HasConversion<string>();

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
}