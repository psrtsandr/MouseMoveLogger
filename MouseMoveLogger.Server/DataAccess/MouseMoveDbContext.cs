using Microsoft.EntityFrameworkCore;
using MouseMoveLogger.Server.Models;

namespace MouseMoveLogger.Server.DataAccess;

public class MouseMoveDbContext(DbContextOptions<MouseMoveDbContext> options) : DbContext(options)
{
    public DbSet<MouseMoveEntry> MouseMoveEntries => Set<MouseMoveEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<MouseMoveEntry>(b =>
        {
            b.ToTable("MouseMoveEntries");
            b.HasKey(e => e.Id);
            b.OwnsMany(e => e.Events, ev => ev.ToJson());
        });
    }
}
