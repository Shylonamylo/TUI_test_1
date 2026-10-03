namespace TUI_Lib.ConsoleGraphics.Colors;

public class Style(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
{
    public ConsoleColor ForegroundColor { get; set; } = foregroundColor;
    public ConsoleColor BackgroundColor { get; set; } = backgroundColor;
    public Style GetInverted()
    {
        return new Style(BackgroundColor, ForegroundColor);
    }
}