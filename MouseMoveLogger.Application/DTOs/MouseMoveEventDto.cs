using static MouseMoveLogger.Domain.Models.MouseMoveEntry;

namespace MouseMoveLogger.Application.DTOs;

public record MouseMoveEventDto(double X, double Y, DateTime T)
{
    public MouseMoveEvent ToModel() => new(X, Y, T);
    public static MouseMoveEvent ToModel(MouseMoveEventDto dto) => dto.ToModel();
    public static MouseMoveEventDto FromModel(MouseMoveEvent e) => new(e.X, e.Y, e.T);
}
