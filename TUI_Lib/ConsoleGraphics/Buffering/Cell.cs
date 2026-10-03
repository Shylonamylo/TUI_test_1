using TUI_Lib.ConsoleGraphics.Colors;

namespace TUI_Lib.ConsoleGraphics.Buffering;

public class Cell
{
    public char Char { get; set; }
    public Style Style;
        
        
    public Cell(char c, Style style)
    {
        Char = c;
        Style = style;
    }
}