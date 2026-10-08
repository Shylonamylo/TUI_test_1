using TUI_Lib.ConsoleGraphics.Colors;

namespace TUI_Lib.ConsoleGraphics.Buffering;

public class Canvas
{
    private Cell[,] _currentCanvas;
    private Cell[,] _previousCanvas;
    
    public int Width { get; }
    public int Height { get; }

    public Canvas(int width, int height)
    {
        _currentCanvas = new Cell[width, height];
        _previousCanvas = new Cell[width, height];
        Width = width;
        Height = height;
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _currentCanvas[x, y] = new Cell('\0', new Style(ConsoleColor.Black, ConsoleColor.White));
                _previousCanvas[x, y] = new Cell('\0', new Style(ConsoleColor.Black, ConsoleColor.White));
            }
        }
    }

    private void Clear()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Set(x, y, '\0', Style.DefaultInverted);
            }
        }
    }

    public void Commit()
    {
        (_currentCanvas, _previousCanvas) = (_previousCanvas, _currentCanvas);
        Clear();
    }

    public bool Set(int x, int y, char c, Style style)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            _currentCanvas[x, y] = new Cell(c, style);

            return true;
        }
        else
        {
            return false;
        }
    }

    public Cell GetPixel(int x, int y)
    {
        return _currentCanvas[x, y];
    }

    public bool HasChanged(int x, int y)
    {
        return _currentCanvas[x, y].Char != _previousCanvas[x, y].Char || 
               _currentCanvas[x, y].Style.BackgroundColor != _previousCanvas[x, y].Style.BackgroundColor || 
               _currentCanvas[x, y].Style.ForegroundColor != _previousCanvas[x, y].Style.ForegroundColor;
    }
    
}