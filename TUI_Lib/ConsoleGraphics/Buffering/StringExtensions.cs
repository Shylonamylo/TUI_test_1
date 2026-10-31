using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.Interfaces;

namespace TUI_Lib.ConsoleGraphics.Buffering;

public static class StringExtensions
{
    public static void WriteString(this IDrawableCanvas canvas, int x, int y, string str, Style style)
    {
        for (int i = 0; i < str.Length; i++)
        {
            canvas.SetCell(x+i, y, str[i], style);
        }
    }

    public static void WriteStringLines(this IDrawableCanvas canvas, int x, int y, string[] lines, Style style)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            canvas.WriteString(x, y+i, lines[i], style);
        }
    }
    public static void WriteStringWrapped(this IDrawableCanvas canvas, int x, int y, string text, int maxLength, Style style)
    {
        for (int i = 0; i < text.Length; i++)
        {
            int col = i % maxLength;
            int row = i / maxLength;
            canvas.SetCell(x+col, y+row, text[i], style);
        }
    }
}