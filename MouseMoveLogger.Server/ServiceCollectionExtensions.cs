using Microsoft.EntityFrameworkCore;
using MouseMoveLogger.Server.DataAccess;

namespace MouseMoveLogger.Server;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMouseMoveDbContext(this IServiceCollection services) => services.AddDbContext<MouseMoveDbContext>(options => options.UseSqlite(connectionString: "Data Source=MouseMoveLogger.db;"));
}