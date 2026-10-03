namespace TUI_Lib.ConsoleGraphics;

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
                
                Console.BackgroundColor = cell.Style.BackgroundColor;
                Console.ForegroundColor = cell.Style.ForegroundColor;
                Console.Write(cell.Char);
            }
        }
        canvas.Commit();
    }
}