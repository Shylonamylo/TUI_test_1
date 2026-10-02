using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib.Renderers;

public class TextElementRenderer : IElementRenderer
{
    public void Render(Element element)
    {
        Console.SetCursorPosition((int)element.Position.X, (int)element.Position.Y);
        
        TextElement textElement = (TextElement)element;
        Console.WriteLine(textElement.Text);
    }
}