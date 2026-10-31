using TUI_Lib.ConsoleGraphics.Buffering;
using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib.Renderers;

public class TextBlockRenderer : IElementRenderer
{
    public void Render(Element element, Canvas canvas)
    {
        TextBlock textBlock = (TextBlock)element;
        
        canvas.DrawBorder(textBlock.Position.X, textBlock.Position.Y, textBlock.Size.X, textBlock.Size.Y, textBlock.Border, textBlock.Style);
        
        int textOffset = textBlock.Size.Y - textBlock.GetLinesCount() < 0 ? 0 : textBlock.Size.Y - textBlock.GetLinesCount();
        
        canvas.WriteStringWrapped(textBlock.Position.X+1, textBlock.Position.Y+textOffset-1, textBlock.Text, textBlock.MaxLength, textBlock.Style);
    }
}