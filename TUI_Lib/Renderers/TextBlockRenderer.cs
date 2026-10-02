using TUI_Lib.Elements;
using TUI_Lib.Interfaces;

namespace TUI_Lib.Renderers;

public class TextBlockRenderer : IElementRenderer
{
    public TextBlockRenderer()
    {
        
    }
    public void Render(Element element)
    {
        Console.SetCursorPosition((int)element.Position.X, (int)element.Position.Y);
        
        TextBlock textBlock = (TextBlock)element;
        
        for (int i = 0; i < textBlock.Text.Length + 2; i++)
        {
            Console.Write(textBlock.Border.HorizontalChar);
        }
        
        Console.Write("\n");
        Console.SetCursorPosition((int)element.Position.X, (int)element.Position.Y+1);
        Console.WriteLine(textBlock.Border.VerticalChar + textBlock.Text + textBlock.Border.VerticalChar);
        Console.SetCursorPosition((int)element.Position.X, (int)element.Position.Y+2);
        for (int i = 0; i < textBlock.Text.Length + 2; i++)
        {
            Console.Write(textBlock.Border.HorizontalChar);
        }
    }
}