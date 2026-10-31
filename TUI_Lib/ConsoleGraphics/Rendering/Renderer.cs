using TUI_Lib.ConsoleGraphics.Buffering;

namespace TUI_Lib.ConsoleGraphics.Rendering;

public static class Renderer
{
    public static void Draw(Canvas canvas)
    {
        int maxX = Math.Min(canvas.Width, Console.BufferWidth);
        int maxY = Math.Min(canvas.Height, Console.BufferHeight);
        
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                if(!canvas.HasChanged(x, y)) continue;
                
                try
                {
                    Console.SetCursorPosition(x, y);
                }
                catch
                {
                    continue;
                }
                
                Cell cell = canvas.GetPixel(x, y);

                if (cell.Char != '\0')
                {
                    Console.BackgroundColor = cell.Style.BackgroundColor;
                    Console.ForegroundColor = cell.Style.ForegroundColor;
                    Console.Write(cell.Char);
                }
                else
                {
                    Console.BackgroundColor = cell.Style.ForegroundColor;
                    Console.ForegroundColor = cell.Style.BackgroundColor;
                    Console.Write(cell.Char);
                }
            }
        }
        canvas.Commit();
    }
}