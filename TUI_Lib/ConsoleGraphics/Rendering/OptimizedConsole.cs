using System.Drawing;
using System.Text;

namespace TUI_Lib.ConsoleGraphics.Rendering;

public class OptimizedConsole
{
    private readonly StringBuilder _stringBuilder = new();

    private int _nextX = -1;
    private int _nextY = -1;

    private Color _lastFg = Color.Empty;
    private Color _lastBg = Color.Empty;
    
    public Color ForegroundColor { get; set; } = Color.White;
    public Color BackgroundColor { get; set; } = Color.Black;
    
    public void SetCursorPosition(int x, int y)
    {
        if (x == _nextX && y == _nextY) return;
        
        _stringBuilder.Append($"\x1b[{y+1};{x+1}H");
        
        (_nextX, _nextY) = (x, y);
    }

    public void SetColor(Color foreground, Color background)
    {
        ForegroundColor = foreground;
        BackgroundColor = background;
    }
    
    public void WriteLine(string text)
    {
        AppendColor();
        
        _stringBuilder.Append(text);
        _stringBuilder.Append("\n");
        
        _nextX = -1;
        _nextY = -1;
    }

    public void Write(string text)
    {
        AppendColor();
        
        _stringBuilder.Append(text);
        
        _nextX += text.Length;
        if (_nextX >= Console.BufferWidth) { _nextX = -1; _nextY = -1; }
    }

    private void AppendColor()
    {
        bool fgChanged = ForegroundColor != _lastFg;
        bool bgChanged = BackgroundColor != _lastBg;
        if (!fgChanged && !bgChanged) return;

        _stringBuilder.Append("\x1b[");
        if (fgChanged)
        {
            _stringBuilder.Append($"38;2;{ForegroundColor.R};{ForegroundColor.G};{ForegroundColor.B}");
            if (bgChanged) _stringBuilder.Append(';');
            _lastFg = ForegroundColor;
        }
        if (bgChanged)
        {
            _stringBuilder.Append($"48;2;{BackgroundColor.R};{BackgroundColor.G};{BackgroundColor.B}");
            _lastBg = BackgroundColor;
        }
        _stringBuilder.Append('m');
    }
    
    public void Flush()
    {
        if (_stringBuilder.Length == 0) return;
        
        var str = _stringBuilder.ToString();
        
        Console.Write($"\x1b[?2026h{str}\x1b[?2026l");
        
        _stringBuilder.Clear();
    }
    
    public void Reset()
    {
        _stringBuilder.Clear();
        _lastFg = Color.Empty;
        _lastBg = Color.Empty;
        _nextX = -1;
        _nextY = -1;
        Console.Write("\x1b[0m\x1b[2J\x1b[H");
    }
    
}