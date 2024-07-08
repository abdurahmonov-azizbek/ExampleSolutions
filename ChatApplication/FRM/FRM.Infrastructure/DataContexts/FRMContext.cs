using FRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FRM.Infrastructure.DataContexts;

public class FRMContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Message> Messages => Set<Message>();

    public FRMContext(DbContextOptions options) : base(options)
    {
        this.Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(user => user.Messages)
            .WithOne(message => message.User)
            .HasForeignKey(message => message.UserId);

        modelBuilder.Entity<User>()
            .HasQueryFilter(user => !user.IsDeleted)
            .HasIndex(user => user.UserName).IsUnique();

        modelBuilder.Entity<Message>()
            .HasQueryFilter(message => !message.IsDeleted);
    }
}
