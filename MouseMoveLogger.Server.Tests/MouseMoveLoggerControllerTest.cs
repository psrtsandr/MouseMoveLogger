using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MouseMoveLogger.Application.DTOs;
using MouseMoveLogger.Domain.Models;
using MouseMoveLogger.Infrastructure.DataAccess;
using MouseMoveLogger.Server.Controllers;
using System.Data.Common;

namespace MouseMoveLogger.Server.Tests;

public class MouseMoveLoggerControllerTest
{
    private readonly DbConnection Connection;
    private readonly DbContextOptions<MouseMoveDbContext> Options;

    public MouseMoveLoggerControllerTest()
    {
        Connection = new SqliteConnection("Filename=:memory:");
        Connection.Open();

        Options = new DbContextOptionsBuilder<MouseMoveDbContext>()
            .UseSqlite(Connection)
            .Options;

        var now = DateTime.UtcNow;

        using var context = new MouseMoveDbContext(Options);
        context.Database.EnsureCreated();
        context.MouseMoveEntries.AddRange(
            new MouseMoveEntry()
            {
                Events = [
                    new MouseMoveEntry.MouseMoveEvent(100, 500, now.AddMilliseconds(1)),
                    new MouseMoveEntry.MouseMoveEvent(100, 600, now.AddMilliseconds(2)),
                    new MouseMoveEntry.MouseMoveEvent(200, 400, now.AddMilliseconds(10)),
                    new MouseMoveEntry.MouseMoveEvent(300, 800, now.AddMilliseconds(20)),
                ]
            },
            new MouseMoveEntry() 
            {
                Events = [
                    new MouseMoveEntry.MouseMoveEvent(123, 456, now.AddSeconds(1).AddMilliseconds(1)),
                    new MouseMoveEntry.MouseMoveEvent(789, 987, now.AddSeconds(1).AddMilliseconds(2)),
                    new MouseMoveEntry.MouseMoveEvent(321, 456, now.AddSeconds(1).AddMilliseconds(10)),
                    new MouseMoveEntry.MouseMoveEvent(654, 369, now.AddSeconds(1).AddMilliseconds(20)),
                ]
            },
            new MouseMoveEntry() 
            {
                Events = [
                    new MouseMoveEntry.MouseMoveEvent(566, 821, now.AddMilliseconds(10)),
                    new MouseMoveEntry.MouseMoveEvent(100, 682, now.AddMilliseconds(15)),
                    new MouseMoveEntry.MouseMoveEvent(341, 726, now.AddMilliseconds(20)),
                    new MouseMoveEntry.MouseMoveEvent(951, 800, now.AddMilliseconds(25)),
                ]
            }
            );
        
        context.SaveChanges();
    }

    [Fact]
    public async Task TestGetAllAsync()
    {
        using var context = CreateContext();
        var controller = new MouseMoveLoggerController(context);
        var result = await controller.GetAllAsync();
        
        Assert.NotNull(result);
        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        var entries = Assert.IsAssignableFrom<IEnumerable<MouseMoveEntryDto>>(okResult.Value);
        Assert.Equal(3, entries.Count());
    }

    [Fact]
    public async Task TestCreateAsync()
    {
        var now = DateTime.UtcNow;
        MouseMoveEventDto[] events = [
            new MouseMoveEventDto(672, 821, now.AddMilliseconds(30)),
            new MouseMoveEventDto(100, 618, now.AddMilliseconds(40)),
            new MouseMoveEventDto(735, 927, now.AddMilliseconds(41)),
            new MouseMoveEventDto(348, 800, now.AddMilliseconds(45))
            ];
        
        using var context = CreateContext();
        var controller = new MouseMoveLoggerController(context);
        var result = await controller.CreateAsync(events);

        Assert.NotNull(result);
        var createdResult = result.Result as CreatedResult;
        Assert.NotNull(createdResult);
        Assert.Equal(201, createdResult.StatusCode);
        var entry = Assert.IsType<MouseMoveEntryDto>(createdResult.Value);
        Assert.True(entry.Id != Guid.Empty);
        Assert.True(events.SequenceEqual(entry.Events));
    }

    private MouseMoveDbContext CreateContext() => new(Options);
}