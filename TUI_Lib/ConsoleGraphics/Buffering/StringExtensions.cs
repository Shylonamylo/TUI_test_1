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

    public static void WriteStringLines(this Canvas canvas, int x, int y, string[] lines, Style style)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            canvas.WriteString(x, y+i, lines[i], style);
        }
    }
    public static void WriteStringWrapped(this Canvas canvas, int x, int y, string text, int maxLength, Style style)
    {
        for (int i = 0; i < text.Length; i++)
        {
            int col = i % maxLength;
            int row = i / maxLength;
            canvas.Set(x+col, y+row, text[i], style);
        }
    }
}