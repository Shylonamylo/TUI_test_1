namespace TUI_Lib.ConsoleGraphics.Colors;

public readonly record struct Style(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
{
    public ConsoleColor ForegroundColor { get; } = foregroundColor;
    public ConsoleColor BackgroundColor { get; } = backgroundColor;
    
    public static readonly Style Default = new Style(ConsoleColor.White, ConsoleColor.Black); 
    public static readonly Style DefaultInverted = new Style(ConsoleColor.Black, ConsoleColor.White); 
    
    public Style GetInverted()
    {
        return new Style(BackgroundColor, ForegroundColor);
    }
}