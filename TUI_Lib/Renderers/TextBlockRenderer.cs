using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib.Renderers;

public class TextBlockRenderer(Canvas canvas) : IElementRenderer
{
    public void Render(Element element)
    {
        TextBlock textBlock = (TextBlock)element;
        
        canvas.DrawBox((int)textBlock.Position.X, (int)textBlock.Position.Y, textBlock.Text.Length+2, 3, textBlock.Border.HorizontalChar, textBlock.Border.VerticalChar, textBlock.Style);
        canvas.WriteString((int)textBlock.Position.X+1, (int)textBlock.Position.Y+1, textBlock.Text, textBlock.Style);
    }
}