using static MouseMoveLogger.Server.Models.MouseMoveEntry;

namespace MouseMoveLogger.Server.DTOs;

public record MouseMoveEventDto(double X, double Y, DateTime T)
{
    public MouseMoveEvent ToModel() => new(X, Y, T);
    public static MouseMoveEvent ToModel(MouseMoveEventDto dto) => dto.ToModel();
    public static MouseMoveEventDto FromModel(MouseMoveEvent e) => new(e.X, e.Y, e.T);
}
