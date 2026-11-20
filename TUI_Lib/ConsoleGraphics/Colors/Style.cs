using System.Drawing;

namespace TUI_Lib.ConsoleGraphics.Colors;

public record struct Style(Color ForegroundColor, Color BackgroundColor)
{
    public static readonly Style Default = new Style(Color.White, Color.Black); 
    public static readonly Style DefaultInverted = new Style(Color.Black, Color.White); 
    
    public Style GetInverted() => new Style(BackgroundColor, ForegroundColor);
}