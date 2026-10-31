using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Colors;

namespace TUI_Lib.Interfaces;

public interface IDrawableCanvas
{
    public int Width { get; }
    public int Height { get; }
    public bool SetCell(int x, int y, char c, Style style);
    public Cell GetCell(int x, int y);
    public void Clear();
    public void Fill(char c, Style style);
}