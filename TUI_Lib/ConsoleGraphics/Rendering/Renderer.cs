using TUI_Lib.ConsoleGraphics.Buffering;

namespace TUI_Lib.ConsoleGraphics.Rendering;

public static class Renderer
{
    public static void Draw(Canvas canvas)
    {
        for (int x = 0; x < canvas.Width; x++)
        {
            for (int y = 0; y < canvas.Height; y++)
            {
                if(!canvas.HasChanged(x, y)) continue;
                Console.SetCursorPosition(x, y);
                
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