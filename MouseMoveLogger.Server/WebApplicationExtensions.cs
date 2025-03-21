using MouseMoveLogger.Infrastructure.DataAccess;

namespace MouseMoveLogger.Server;

public static class WebApplicationExtensions
{
    public static void InitializeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<MouseMoveDbContext>();
        var rnd = new Random();
        context.Database.EnsureCreated();
    }
}
