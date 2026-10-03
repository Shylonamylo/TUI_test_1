using TUI_Lib.ConsoleGraphics.Colors;

namespace TUI_Lib.ConsoleGraphics.Buffering;

public static class StringExtensions
{
    public static void WriteString(this Canvas canvas, int x, int y, string str, Style style)
    {
        for (int i = 0; i < str.Length; i++)
        {
            canvas.Set(x+i, y, str[i], style);
        }
    }
}