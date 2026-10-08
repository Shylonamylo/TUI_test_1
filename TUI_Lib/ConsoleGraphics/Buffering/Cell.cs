using TUI_Lib.ConsoleGraphics.Colors;

namespace TUI_Lib.ConsoleGraphics.Buffering;

public readonly record struct Cell
{
    public readonly char Char;
    public readonly Style Style;
        
        
    public Cell(char c, Style style)
    {
        Char = c;
        Style = style;
    }
}