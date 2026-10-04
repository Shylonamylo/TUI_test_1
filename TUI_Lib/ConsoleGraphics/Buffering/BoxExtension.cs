using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.Elements;

namespace TUI_Lib.ConsoleGraphics.Buffering;

public static class BoxExtension
{
    public static void DrawBox(this Canvas canvas, int x, int y, int width, int height, char horizontalChar, char verticalChar, Style style)
    {
        for (int i = 0; i < width; i++)
        {
            canvas.Set(x+i, y, horizontalChar, style);
            canvas.Set(x+i, y+2, horizontalChar, style);
        }
        for (int i = 1; i < height-1; i++)
        {
            canvas.Set(x, y+i, verticalChar, style);
            canvas.Set(x+width-1, y+i, verticalChar, style);
        }
    }
}