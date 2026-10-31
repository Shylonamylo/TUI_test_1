using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.ConsoleGraphics.Rendering;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib.Renderers;

public class ButtonRenderer(Canvas canvas) : IElementRenderer
{
    public void Render(Element element)
    {
        Button button = (Button)element;

        canvas.DrawBorder(button.Position.X, button.Position.Y, button.Label.Length+2, button.Size.Y, button.Border, button.Style);

        int textOffset = button.Size.Y - 2 < 0 ? 0 : 1;
        
        canvas.WriteString(button.Position.X+1, button.Position.Y+textOffset, $"{button.Label}", button.Selected?button.Style.GetInverted():button.Style);
    }
}