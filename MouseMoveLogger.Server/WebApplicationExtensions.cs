using MouseMoveLogger.Server.DataAccess;
using MouseMoveLogger.Server.Models;

namespace MouseMoveLogger.Server;

public static class WebApplicationExtensions
{
    public static void InitializeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<MouseMoveDbContext>();
        var rnd = new Random();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        var entries = Enumerable.Range(0, 10)
            .Select(i => new MouseMoveEntry()
            {
                Events = [.. Enumerable.Range(0, 10).Select(i => new MouseMoveEntry.MouseMoveEvent(rnd.NextDouble() * 1000, rnd.NextDouble() * 1000, DateTime.Now))]
            })
            .ToList();
        context.MouseMoveEntries.AddRange(entries);
        context.SaveChanges();
    }
}
