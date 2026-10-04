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

        canvas.DrawBox((int)button.Position.X, (int)button.Position.Y, button.Label.Length+2, 3, button.Border.HorizontalChar, button.Border.VerticalChar, button.Style);
        canvas.WriteString((int)button.Position.X+1, (int)button.Position.Y+1, $"{button.Label}", button.Selected?button.Style.GetInverted():button.Style);
    }

}