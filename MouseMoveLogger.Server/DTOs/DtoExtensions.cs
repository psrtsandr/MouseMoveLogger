using MouseMoveLogger.Server.Models;
using static MouseMoveLogger.Server.Models.MouseMoveEntry;

namespace MouseMoveLogger.Server.DTOs;

public static class DtoExtensions
{
    public static IQueryable<MouseMoveEntryDto> ToDto(this IQueryable<MouseMoveEntry> entries) => entries.Select(e => MouseMoveEntryDto.FromModel(e));
    public static MouseMoveEntry ToModel(this IEnumerable<MouseMoveEventDto> dtos) => new() { Events = ToModelInternal(dtos) };
    private static ICollection<MouseMoveEvent> ToModelInternal(IEnumerable<MouseMoveEventDto> dtos) => [.. dtos.Select(MouseMoveEventDto.ToModel)];
}
