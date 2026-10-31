using System.Text;
using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Colors;

namespace TUI_Lib.ConsoleGraphics.Rendering;

public static class Renderer
{
    public static void Draw(Canvas canvas)
    {
        int maxX = Math.Min(canvas.Width, Console.BufferWidth);
        int maxY = Math.Min(canvas.Height, Console.BufferHeight);
        
        StringBuilder stringBuilder = new StringBuilder();

        for (int y = 0; y < maxY; y++)
        {
            int startX = -1;
            Style? lastStyle = null;
            
            for (int x = 0; x < maxX; x++)
            {
                Cell cell = canvas.GetCell(x, y);
                
                if (canvas.HasChanged(x, y))
                {
                    if (startX == -1)
                    {
                        startX = x;
                        lastStyle = cell.Style;
                        stringBuilder.Append(cell.Char);
                    }
                    else
                    {
                        if (cell.Style != lastStyle)
                        {
                            Flush(startX, y, stringBuilder, lastStyle ?? Style.Default);
                            startX = x;
                            lastStyle = cell.Style;
                            stringBuilder.Clear();
                            stringBuilder.Append(cell.Char);
                        }
                        else
                        {
                            stringBuilder.Append(cell.Char);
                        }
                    }
                }
                else
                {
                    if (startX != -1)
                    {
                        Flush(startX, y, stringBuilder, lastStyle ?? Style.Default);
                        startX = -1;
                        stringBuilder.Clear();
                    }
                }
            }
            if (startX != -1)
            {
                Flush(startX, y, stringBuilder, lastStyle ?? Style.Default);
                stringBuilder.Clear();
            }
        }
        
        /*for (int x = 0; x < maxX; x++)
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
                
                Cell cell = canvas.GetCell(x, y);

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
        }*/
        
        
        canvas.Commit();
    }

    private static void Flush(int x, int y, StringBuilder text, Style style)
    {
        if(text.Length == 0) return;

        try 
        {
            if (Console.ForegroundColor != style.ForegroundColor)
                Console.ForegroundColor = style.ForegroundColor;

            if (Console.BackgroundColor != style.BackgroundColor)
                Console.BackgroundColor = style.BackgroundColor;
        
            Console.SetCursorPosition(x, y);
            Console.Write(text.ToString());
        }
        catch
        {
            // ignored
        }
    }
}