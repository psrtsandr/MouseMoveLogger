namespace MouseMoveLogger.Server.Models;

public class MouseMoveEntry
{
    public record MouseMoveEvent(double X, double Y, DateTime T);
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required ICollection<MouseMoveEvent> Events { get; set; } = [];
}
