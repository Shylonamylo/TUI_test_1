using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.ConsoleGraphics.Types;
using TUI_Lib.Interfaces;

namespace TUI_Lib.ConsoleGraphics.Buffering;

public static class LineExtension
{
    public static void DrawLine(this IDrawableCanvas canvas, Vec2I start, Vec2I end, char c, Style style)
    {
        
        int x1 = start.X;
        int x2 = end.X;
        int y1 = start.Y;
        int y2 = end.Y;
        
        int deltaX = Math.Abs(x2 - x1);
        int deltaY = Math.Abs(y2 - y1);
        
        int signX = x1 < x2 ? 1 : -1;
        int signY = y1 < y2 ? 1 : -1;
        
        int deltaErr = deltaX - deltaY;
        
        canvas.SetCell(x2, y2, c, style);
        
        while(x1 != x2 || y1 != y2) 
        {
            canvas.SetCell(x1, y1, c, style);
            
            int d2 = deltaErr * 2;
            
            if(d2 > -deltaY) 
            {
                deltaErr -= deltaY;
                x1 += signX;
            }
            
            if(d2 < deltaX) 
            {
                deltaErr += deltaX;
                y1 += signY;
            }
        }
    }
}