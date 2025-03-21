using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MouseMoveLogger.Application.DTOs;
using MouseMoveLogger.Infrastructure.DataAccess;

namespace MouseMoveLogger.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MouseMoveLoggerController(MouseMoveDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MouseMoveEntryDto>>> GetAllAsync()
    {
        var result = await context.MouseMoveEntries
            .ToDto()
            .ToListAsync();
        return Ok(result);  
    }

    [HttpPost]
    public async Task<ActionResult<MouseMoveEntryDto>> CreateAsync([FromBody] IEnumerable<MouseMoveEventDto> events)
    {
        var entry = events.ToModel();
        context.MouseMoveEntries.Add(entry);
        await context.SaveChangesAsync();
        return Created(string.Empty, MouseMoveEntryDto.FromModel(entry));
    }
}
