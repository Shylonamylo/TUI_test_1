using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.ConsoleGraphics.Types;
using TUI_Lib.Interfaces;

namespace TUI_Lib.Elements;

public class UserCanvas : Element, IDrawableCanvas
{
    public Border? Border { get; set; } = Border.Default;
    
    public int Width => _canvas.Width;
    public int Height => _canvas.Height;

    public override Vec2I Size
    {
        get
        {
            if (Border == null)
                return new Vec2I(Width, Height);
            else if (Border.FullSize)
                return new Vec2I(Width + 2, Height+2);
            else
                return new Vec2I(Width + 2, Height);
        }
    }

    private Canvas _canvas;

    public UserCanvas(int width, int height)
    {
        _canvas = new Canvas(width, height);
        Fill('\0', Style.DefaultInverted);
    }
    
    public bool SetCell(int x, int y, char c, Style style)
    {
        return _canvas.SetCell(x, y, c, style);
    }

    public Cell GetCell(int x, int y)
    {
        return _canvas.GetCell(x, y);
    }

    public void Clear()
    {
        _canvas.Clear();
    }

    public void Fill(char c, Style style)
    {
        _canvas.Fill(c, style);
    }
}