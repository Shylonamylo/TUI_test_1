using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;
using TUI_test_1.CustomElements.Elements;

namespace TUI_test_1.CustomElements.Renderers;

public class TestElementRenderer : IElementRenderer
{
    public void Render(Element element, Canvas canvas)
    {
        TestElement testElement = (TestElement)element;
        canvas.WriteString(testElement.Position.X, testElement.Position.Y, testElement.Text, testElement.Style);
    }
}