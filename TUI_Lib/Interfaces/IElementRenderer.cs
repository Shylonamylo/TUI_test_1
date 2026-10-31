using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.Elements;

namespace TUI_Lib.Interfaces;

public interface IElementRenderer
{
    public void Render(Element element, Canvas canvas);
}