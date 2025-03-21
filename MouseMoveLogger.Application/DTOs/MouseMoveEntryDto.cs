using MouseMoveLogger.Domain.Models;

namespace MouseMoveLogger.Application.DTOs;

public record MouseMoveEntryDto(Guid Id, IEnumerable<MouseMoveEventDto> Events)
{
    public static MouseMoveEntryDto FromModel(MouseMoveEntry entry) => new(entry.Id, entry.Events.Select(MouseMoveEventDto.FromModel));
}
