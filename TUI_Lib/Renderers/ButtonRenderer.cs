using TUI_Lib.ConsoleGraphics;
using TUI_Lib.ConsoleGraphics.Colors;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib.Renderers;

public class ButtonRenderer(Canvas canvas) : IElementRenderer
{
    public void Render(Element element)
    {
        Button button = (Button)element;
        
        for (int i = 0; i < button.Label.Length + 2; i++)
        {
            canvas.Set((int)button.Position.X+i, (int)button.Position.Y, button.Border.HorizontalChar, element.Style);
        }

        canvas.WriteString((int)button.Position.X, (int)button.Position.Y+1, $"{button.Border.VerticalChar}{button.Label}{button.Border.VerticalChar}", button.Selected?button.Style.GetInverted():button.Style);
        
        for (int i = 0; i < button.Label.Length + 2; i++)
        {
            canvas.Set((int)button.Position.X+i, (int)button.Position.Y+2, button.Border.HorizontalChar, element.Style);
        }
    }

}