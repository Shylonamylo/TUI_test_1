using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.Interfaces;

namespace TUI_Lib.ConsoleGraphics.Buffering;

public class Canvas: IDrawableCanvas
{
    private Cell[,] _currentCanvas;
    private Cell[,] _previousCanvas;
    
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Canvas(int width, int height)
    {
        Width = width;
        Height = height;
        
        Resize(width, height);
    }

    public void Clear()
    {
        Fill(' ', Style.Default);
    }

    public void Fill(char c, Style style)
    {
        var emptyCell = new Cell(c, style);
        
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _currentCanvas[x, y] = emptyCell;
            }
        }
    }

    public void Commit()
    {
        (_currentCanvas, _previousCanvas) = (_previousCanvas, _currentCanvas);
        Clear();
    }

    public bool SetCell(int x, int y, char c, Style style)
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

    public Cell GetCell(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return new Cell();
        }
        return _currentCanvas[x, y];
    }

    public bool HasChanged(int x, int y)
    {
        return _currentCanvas[x, y].Char != _previousCanvas[x, y].Char || 
               _currentCanvas[x, y].Style.BackgroundColor != _previousCanvas[x, y].Style.BackgroundColor || 
               _currentCanvas[x, y].Style.ForegroundColor != _previousCanvas[x, y].Style.ForegroundColor;
    }

    public void Resize(int width, int height)
    {
        _currentCanvas = new Cell[width, height];
        _previousCanvas = new Cell[width, height];
        
        Width = width;
        Height = height;
    }
    
}