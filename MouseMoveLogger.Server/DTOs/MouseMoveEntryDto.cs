using MouseMoveLogger.Server.Models;

namespace MouseMoveLogger.Server.DTOs;

public record MouseMoveEntryDto(Guid Id, IEnumerable<MouseMoveEventDto> Events)
{
    public static MouseMoveEntryDto FromModel(MouseMoveEntry entry) => new(entry.Id, entry.Events.Select(MouseMoveEventDto.FromModel));
}
