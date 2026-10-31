using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib.ConsoleGraphics.Buffering;

public static class BoxExtensions
{
    public static void DrawBox(this IDrawableCanvas canvas, int x, int y, int width, int height, char horizontalChar, char verticalChar, Style style)
    {
        for (int i = 0; i < width; i++)
        {
            canvas.SetCell(x+i, y, horizontalChar, style);
            canvas.SetCell(x+i, y+height-1, horizontalChar, style);
        }
        for (int i = 1; i < height-1; i++)
        {
            canvas.SetCell(x, y+i, verticalChar, style);
            canvas.SetCell(x+width-1, y+i, verticalChar, style);
        }
    }
}