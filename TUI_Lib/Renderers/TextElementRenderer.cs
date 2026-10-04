using TUI_Lib.ConsoleGraphics;
using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.ConsoleGraphics.Rendering;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib.Renderers;

public class TextElementRenderer(Canvas canvas) :  IElementRenderer
{
    
    public void Render(Element element)
    {
        TextElement textElement = (TextElement)element;
        canvas.WriteString((int)textElement.Position.X, (int)textElement.Position.Y, textElement.Text, textElement.Style);
    }
}