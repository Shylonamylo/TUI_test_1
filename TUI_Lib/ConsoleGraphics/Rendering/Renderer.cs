using System.Text;
using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Colors;

namespace TUI_Lib.ConsoleGraphics.Rendering;

public class Renderer
{
    private readonly OptimizedConsole _optimizedConsole = new();
    private readonly StringBuilder _stringBuilder = new();
    public void Draw(Canvas canvas)
    {
        int maxX = Math.Min(canvas.Width, Console.WindowWidth);
        int maxY = Math.Min(canvas.Height, Console.WindowHeight);

        for (int y = 0; y < maxY; y++)
        {
            int startX = -1;
            Style? lastStyle = null;
            
            for (int x = 0; x < maxX; x++)
            {
                if (canvas.HasChanged(x, y))
                {
                    Cell cell = canvas.GetCell(x, y);
                    
                    if (startX == -1)
                    {
                        startX = x;
                        lastStyle = cell.Style;
                        _stringBuilder.Append(cell.Char);
                    }
                    else
                    {
                        if (cell.Style != lastStyle)
                        {
                            Flush(startX, y, _stringBuilder, lastStyle ?? Style.Default);
                            startX = x;
                            lastStyle = cell.Style;
                            _stringBuilder.Clear();
                            _stringBuilder.Append(cell.Char);
                        }
                        else
                        {
                            _stringBuilder.Append(cell.Char);
                        }
                    }
                }
                else
                {
                    if (startX != -1)
                    {
                        Flush(startX, y, _stringBuilder, lastStyle ?? Style.Default);
                        startX = -1;
                        _stringBuilder.Clear();
                    }
                }
            }
            if (startX != -1)
            {
                Flush(startX, y, _stringBuilder, lastStyle ?? Style.Default);
                _stringBuilder.Clear();
            }
        }
        _optimizedConsole.Flush();
        canvas.Commit();
        _stringBuilder.Clear();
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