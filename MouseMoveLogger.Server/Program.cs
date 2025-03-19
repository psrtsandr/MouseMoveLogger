using MouseMoveLogger.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(p => p.AddPolicy("CorsPolicy", o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddMouseMoveDbContext();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.InitializeDatabase();

app.Run();
