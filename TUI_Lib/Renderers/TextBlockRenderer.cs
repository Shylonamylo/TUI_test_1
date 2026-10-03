using TUI_Lib.ConsoleGraphics;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib.Renderers;

public class TextBlockRenderer(Canvas canvas) : IElementRenderer
{
    public void Render(Element element)
    {
        TextBlock textBlock = (TextBlock)element;
        
        for (int i = 0; i < textBlock.Text.Length + 2; i++)
        {
            canvas.Set((int)textBlock.Position.X+i, (int)textBlock.Position.Y, textBlock.Border.HorizontalChar, element.Style);
        }
        
        canvas.WriteString((int)textBlock.Position.X, (int)textBlock.Position.Y+1, $"{textBlock.Border.VerticalChar}{textBlock.Text}{textBlock.Border.VerticalChar}", element.Style);
        
        for (int i = 0; i < textBlock.Text.Length + 2; i++)
        {
            canvas.Set((int)textBlock.Position.X+i, (int)textBlock.Position.Y+2, textBlock.Border.HorizontalChar, element.Style);
        }
    }
}