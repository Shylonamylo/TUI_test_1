using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib.ConsoleGraphics.Buffering;

public static class BorderExtensions
{
    public static void DrawBorder(this IDrawableCanvas canvas, int x, int y, int width, int height, Border? border, Style style)
    {
        if(border == null) return;
        
        if  (border.FullSize)
        {
            canvas.DrawBox(x, y, width, height, (char)border.HorizontalChar!, (char)border.VerticalChar!, style);
            
            canvas.SetCell(x, y, (char)border.LeftUpCorner!, style);
            canvas.SetCell(x+width-1, y, (char)border.RightUpCorner!, style);
            canvas.SetCell(x, y+height-1, (char)border.LeftDownCorner!, style);
            canvas.SetCell(x+width-1, y+height-1, (char)border.RightDownCorner!, style);
        }
        else
        {
            for (int i = 0; i < height; i++)
            {
                canvas.SetCell(x, y+i, (char)border.VerticalChar!, style);
                canvas.SetCell(x+width-1, y+i, (char)border.VerticalChar!, style);
            }
        }
        
    }
}