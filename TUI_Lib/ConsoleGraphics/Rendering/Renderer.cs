using System.Text;
using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Colors;

namespace TUI_Lib.ConsoleGraphics.Rendering;

public class Renderer
{
    private readonly OptimizedConsole _optimizedConsole = new();
    public void Draw(Canvas canvas)
    {
        int maxX = Math.Min(canvas.Width, Console.BufferWidth - 1);
        int maxY = Math.Min(canvas.Height, Console.BufferHeight - 1);
        
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
        _optimizedConsole.Flush();
        canvas.Commit();
    }

    private void Flush(int x, int y, StringBuilder text, Style style)
    {
        if(text.Length == 0) return;

        try 
        {
            _optimizedConsole.ForegroundColor = style.ForegroundColor;
            _optimizedConsole.BackgroundColor = style.BackgroundColor;
        
            _optimizedConsole.SetCursorPosition(x, y);
            _optimizedConsole.Write(text.ToString());
        }
        catch
        {
            // ignored
        }
    }

    public void Reset() => _optimizedConsole.Reset();
}